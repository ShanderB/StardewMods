﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewValley;
using StardewValley.Menus;

namespace Pathoschild.LookupAnything.Components
{
    /// <summary>A textbox fires events while searching.</summary>
    internal class SearchTextBox : IKeyboardSubscriber
    {
        /*********
        ** Properties
        *********/
        /// <summary>The underlying textbox.</summary>
        private readonly TextBox Textbox;

        /// <summary>The last search text received for change detection.</summary>
        private string LastText = string.Empty;


        /*********
        ** Accessors
        *********/
        /// <summary>The event raised when the search text changes.</summary>
        public event EventHandler<string> OnChanged;

        /// <summary>The input text.</summary>
        public string Text
        {
            get { return this.Textbox.Text; }
            set { this.Textbox.Text = value; }
        }

        /// <summary>Whether the focus is in the textbox.</summary>
        public bool Selected
        {
            get { return this.Textbox.Selected; }
            set { this.Textbox.Selected = value; }
        }

        /// <summary>The X position of the rendered textbox.</summary>
        public int X
        {
            get { return this.Textbox.X; }
            set { this.Textbox.X = value; }
        }

        /// <summary>The Y position of the rendered textbox.</summary>
        public int Y
        {
            get { return this.Textbox.Y; }
            set { this.Textbox.Y = value; }
        }

        /// <summary>The width of the rendered textbox.</summary>
        public int Width
        {
            get { return this.Textbox.Width; }
            set { this.Textbox.Width = value; }
        }

        /// <summary>The height of the rendered textbox.</summary>
        public int Height
        {
            get { return this.Textbox.Height; }
            set { this.Textbox.Height = value; }
        }


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="font">The text font.</param>
        /// <param name="textColor">The text color.</param>
        public SearchTextBox(SpriteFont font, Color textColor)
        {
            this.Textbox = new TextBox(Sprites.Textbox.Sheet, null, font, textColor);
        }

        /// <summary>Set the input focus to this control.</summary>
        public void Select()
        {
            this.Textbox.Selected = true;
            this.Textbox.Highlighted = true;
            Game1.keyboardDispatcher.Subscriber = this;
        }

        /// <summary>Receive input from the user.</summary>
        /// <param name="inputChar">The input character.</param>
        public void RecieveTextInput(char inputChar)
        {
            this.Textbox.RecieveTextInput(inputChar);
            this.NotifyChange();
        }

        /// <summary>Receive input from the user.</summary>
        /// <param name="text">The input text.</param>
        public void RecieveTextInput(string text)
        {
            this.Textbox.RecieveTextInput(text);
            this.NotifyChange();
        }

        /// <summary>Receive input from the user.</summary>
        /// <param name="command">The input command.</param>
        public void RecieveCommandInput(char command)
        {
            this.Textbox.RecieveCommandInput(command);
            this.NotifyChange();
        }

        /// <summary>Receive input from the user.</summary>
        /// <param name="key">The input key.</param>
        public void RecieveSpecialInput(Keys key)
        {
            this.Textbox.RecieveSpecialInput(key);
            this.NotifyChange();
        }

        /// <summary>Draw the textbox.</summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            this.Textbox.Draw(batch);
        }

        /// <summary>Detect updated search text and notify listeners.</summary>
        private void NotifyChange()
        {
            if (this.Text != this.LastText)
            {
                this.OnChanged?.Invoke(this, this.Text);
                this.LastText = this.Text;
            }
        }
    }
}
