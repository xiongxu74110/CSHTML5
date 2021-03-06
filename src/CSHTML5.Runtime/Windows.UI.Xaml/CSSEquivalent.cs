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

#if MIGRATION
namespace System.Windows
#else
namespace Windows.UI.Xaml
#endif
{
    internal class CSSEquivalent
    {
        internal List<string> Name;
        /// <summary>
        /// true means that all we will always use Velocity to apply the value to the dom Element, even when there is no animation. This is useful when the name used in Velocity is different than the name we would normally use and when Velocity ignores changes that happen outside of Velocity. See ScaleTransform.ScaleX
        /// </summary>
        internal bool OnlyUseVelocity = false;
        internal object DomElement;
        internal bool ApplyAlsoWhenThereIsAControlTemplate = false; // Note: normally, this should never be True, because DependencyProperty inheritance result in the property being passed to the child controls. However, in some cases such as "Control.Foreground" or "Control.FontSize", for performance reasons, we do not use the XAML-based property inheritance, but instead we use the HTML DOM inheritance of such CSS properties. In those cases we need to set this boolean to True.
        internal ValueToHtmlConverter Value;
        internal ValueToMultipleValuesHtmlConverter Values;
        internal UIElement UIElement; // This is the control which appearance we modify. If not specified, the instance on which the CSSEquivalent is obtained is used. This property is used mainly to ensure that the UIElement has no ControlTemplate (otherwise we must not apply the CSS changes)

        //-----------------------------------Special case where CSSEquivalent does not exist on the property but we still need it -----------------------------------//

        //Note: we still need the CSSEuivalent in this case for the GetCSSEquivalents on IHasAccessToPropertiesWhereItIsUsed element because the elements in the collection might not have defined GetCSSEquivalent for the property that uses the IHasAccessToPropertiesWhereItIsUsed.
        //todo: see if the Callback method below should be called everytime when defined or if we should define a boolean telling us to use it or not.
        internal PropertyChangedCallback CallbackMethod; //this method is here to allow us to handle properties that require more than just setting a value through javascript (for example, a change in Shape.Fill requires a call to the ScheduleRedraw method).
        internal DependencyProperty DependencyProperty; //this is required to be able to call the CallbackMethod.
    }
}
