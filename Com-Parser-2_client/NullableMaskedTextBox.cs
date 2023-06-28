using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    class NullableMaskedTextBox : MaskedTextBox
    {
        [Category(nameof(CategoryAttribute.Behavior))]
        [Description("Разделитель, используемый в свойстве Mask")]
        public char Delimiter { set; get; }

        private bool NotValidChar;

        public new bool MaskCompleted { private set; get; }

        public delegate bool ValidateFunction(string input);

        private ValidateFunction Validator = (input) => true;

        public NullableMaskedTextBox()
        {
            MaskInputRejected += new MaskInputRejectedEventHandler(InputRejected);
        }

        public void TextValidator(ValidateFunction validator)
        {
            Validator = validator;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            if (Validator != null)
            {
                MaskCompleted = Validator(Text);
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (!MaskCompleted && ForeColor == Color.Red)
            {
                ForeColor = Color.Black;
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            if (Validator != null && !Validator(Text))
            {
                ForeColor = Color.Red;
                MaskCompleted = false;
                return;
            }
            
            MaskCompleted = true;

            base.OnValidating(e);
        }

        private void InputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            NotValidChar = true;
        }

        private void SkipToDelimiter(KeyPressEventArgs e)
        {
            if (NotValidChar && e.KeyChar == Delimiter)
            {
                int targetIndex = Text.IndexOf(e.KeyChar, SelectionStart);

                if (targetIndex != -1)
                {
                    SelectionStart = Math.Min(targetIndex + 1, Text.Length);
                }

                e.Handled = true;
            }
        }

        private void SkipPromptCharsWhileBackspace(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && SelectionStart > 0 && Text[SelectionStart - 1] == PromptChar)
            {
                while (SelectionStart > 0 && Text[SelectionStart - 1] != Delimiter)
                {
                    SelectionStart--;
                }

                e.Handled = true;
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            NotValidChar = false;

            base.OnKeyPress(e);

            SkipToDelimiter(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            SkipPromptCharsWhileBackspace(e);
        }
    }
}
