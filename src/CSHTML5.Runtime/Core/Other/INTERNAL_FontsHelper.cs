﻿
//===============================================================================
//
//  IMPORTANT NOTICE, PLEASE READ CAREFULLY:
//
//  ● This code is dual-licensed (GPLv3 + Commercial). Commercial licenses can be obtained from: http://cshtml5.com
//
//  ● You are NOT allowed to:
//       – Use this code in a proprietary or closed-source project (unless you have obtained a commercial license)
//       – Mix this code with non-GPL-licensed code (such as MIT-licensed code), or distribute it under a different license
//       – Remove or modify this notice
//
//  ● Copyright 2019 Userware/CSHTML5. This code is part of the CSHTML5 product.
//
//===============================================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHTML5.Internal
{
    internal static class INTERNAL_FontsHelper
    {
        static Dictionary<string, string> _loadedFonts = new Dictionary<string, string>();
        static int _suffixesUsed = 0; //this counts the amount of fonts we added (the name comes from the fact that this is what we use as a suffix for the names to use for the fonts).

        /// <summary>
        /// Creates a css style with @font-face for the given font, then returns the name to use as the font-family. This name is actually the relative path to the file containing the font.
        /// </summary>
        /// <param name="fontPath">The path to the font.</param>
        /// <returns>The name to use to set the font-family property, a.k.a: the relative path to the font.</returns>
        internal static string LoadFont(string fontPath)
        {
            if(!fontPath.Contains('.')) //Note: if the path does not contain the character '.', then it means that there is no specified file. It is therefore a default font or thet path to a folder containing fonts, which we cannot handle so we simply return the font as is.
            {
                return fontPath;
            }
            string fontPathWithoutCustomName = fontPath;
            int indexOfHashTag = fontPath.IndexOf('#');
            if(indexOfHashTag != -1)
            {
                fontPathWithoutCustomName = fontPath.Substring(0, indexOfHashTag);
            }

            string lowerCasePathWithoutCustomName = fontPathWithoutCustomName.ToLower();

            //we try to make the path fit by considering that it is the startup assembly if not specifically defined otherwise: (basically add a prefix ms-appx if there is none)
            //Note: we should not enter the "if" below if the path was defined in xaml. This cas is already handled during compilation.
            if (!(lowerCasePathWithoutCustomName.StartsWith(@"ms-appx:/")
                || lowerCasePathWithoutCustomName.StartsWith(@"http://")
                || lowerCasePathWithoutCustomName.StartsWith(@"https://")
                || fontPathWithoutCustomName.Contains(@";component/")))
            {
                fontPathWithoutCustomName = "ms-appx:/" + fontPathWithoutCustomName;
            }

            //Get a path that will lead to the position of the file:
            string relativeFontPath = INTERNAL_UriHelper.ConvertToHtml5Path(fontPathWithoutCustomName, "");

            if (_loadedFonts.ContainsKey(relativeFontPath))
            {
                return _loadedFonts[relativeFontPath];
            }
            return CreateFontInJS(relativeFontPath); //note: this adds the relativeFontPath to _loadedFonts.
        }

        static string CreateFontInJS(string fontUri)
        {
            string fontName = GetNewFontName();
            fontUri = fontUri.Replace('\\', '/'); //this is required.
            if (fontUri.StartsWith("/")) //this breaks it for some reason.
            {
                fontUri = fontUri.Substring(1);
            }
            CSHTML5.Interop.ExecuteJavaScript(@"var newStyle = document.createElement('style');
newStyle.appendChild(document.createTextNode(""\
@font-face {\
    font-family: '"" + $1 + ""';\
    src: url('"" + $0 + ""')\
}}\
""));
document.body.appendChild(newStyle);", fontUri, fontName); //Note: we used src: url(...) because src: local(...) does not seem to work. Also, we added the css style to the body instead of the header (which is what I found everywhere non the internet) because it didn't seem to work for me.

            _loadedFonts.Add(fontUri, fontName);
            return fontName;
        }

        static string GetNewFontName()
        {
            //generate a string with a suffix and return it:
            string suffixesUsedAsString = _suffixesUsed.ToString();
            ++_suffixesUsed;
            return "fontName" + suffixesUsedAsString;
        }
    }
}
