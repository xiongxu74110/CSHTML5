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

using System.Diagnostics;
using System.Windows;

#if MIGRATION
using SW = Microsoft.Windows;
using System.Collections.ObjectModel;
#else
using SW = System.Windows;
using Windows.UI.Xaml;
using Windows.Foundation;
#endif

#if MIGRATION
namespace System.Windows.Controls
#else
namespace Windows.UI.Xaml.Controls
#endif
{
    /// <summary>
    /// Information describing a drag event on a UIElement.
    /// </summary>
    public sealed class ItemDragEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ItemDragEventArgs class.
        /// </summary>
        internal ItemDragEventArgs(SelectionCollection data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Gets or sets a value indicating whether an item drag
        /// operation was handled.
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// Gets or sets the effects of the completed drag operation.
        /// </summary>
        public SW.DragDropEffects Effects { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the item container being dragged.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to cancel the action.
        /// </summary>
        public bool Cancel { get; set; }

#if unsupported
        /// <summary>
        /// Gets a value indicating whether removing data
        /// from the source is handled by the target.
        /// </summary>
        public bool DataRemovedFromDragSource { get; private set; }

        /// <summary>
        /// Gets or sets an action that removes data from the drag source.
        /// </summary>
        internal Action<ItemDragEventArgs> RemoveDataFromDragSourceAction { get; set; }

        /// <summary>
        /// This method removes the data from the drag source.
        /// </summary>
        public void RemoveDataFromDragSource()
        {
            if (DataRemovedFromDragSource)
            {
                throw new InvalidOperationException("Properties.Resources.ItemDragEventArgs_RemoveDataFromSource_DataHasAlreadyBeenRemovedFromSource");
            }
            else if ((this.AllowedEffects & SW.DragDropEffects.Move) != SW.DragDropEffects.Move)
            {
                throw new InvalidOperationException("Properties.Resources.ItemDragEventArgs_RemoveDataFromSource_CannotRemoveDataBecauseMoveIsNotAnAllowedEffect");
            }
            else
            {
                RemoveDataFromDragSourceAction(this);
                DataRemovedFromDragSource = true;
            }
        }

        /// <summary>
        /// Gets the key states.
        /// </summary>
        public SW.DragDropKeyStates KeyStates { get; internal set; }

        /// <summary>
        /// Gets or sets the allowed effects.
        /// </summary>
        public SW.DragDropEffects AllowedEffects { get; set; }

        /// <summary>
        /// Gets or sets the control that is the source of the drag.
        /// </summary>
        public DependencyObject DragSource { get; set; }

        /// <summary>
        /// Gets or sets the mouse offset from the item being dragged at the 
        /// beginning of the drag operation.
        /// </summary>
        public Point DragDecoratorContentMouseOffset { get; set; }

        /// <summary>
        /// Gets or sets the content to insert into the DragDecorator.
        /// </summary>
        public object DragDecoratorContent { get; set; }

        /// <summary>
        /// Initializes a new instance of the ItemDragEventArgs class using an
        /// existing instance.
        /// </summary>
        /// <param name="args">The instance to use as the template when creating
        /// the new instance.</param>
        internal ItemDragEventArgs(ItemDragEventArgs args)
        {
            this.AllowedEffects = args.AllowedEffects;
            this.Effects = args.Effects;
            this.Data = args.Data;
            this.DragSource = args.DragSource;
            this.KeyStates = args.KeyStates;
            this.DragDecoratorContent = args.DragDecoratorContent;
            this.DragDecoratorContentMouseOffset = args.DragDecoratorContentMouseOffset;
            this.RemoveDataFromDragSourceAction = args.RemoveDataFromDragSourceAction;
            this.DataRemovedFromDragSource = args.DataRemovedFromDragSource;
        }
#endif
    }
}
