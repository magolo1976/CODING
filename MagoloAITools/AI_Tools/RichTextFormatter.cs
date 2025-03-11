using System.Text.RegularExpressions;
namespace MagoloAITools.AI_Tools
{
    public class RichTextFormatter
    {
        private static RichTextBox _richTextBox;

        public static void FormatText(RichTextBox richTextBox, string text)
        {
            richTextBox.Clear();

            _richTextBox = richTextBox;

            //text = text.Replace("\n\n", "\r\n");

            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                /*if (line.Contains("###"))
                {
                    FormatHeading(line.Substring(4), 3);
                }
                else if (line.Contains("##"))
                {
                    FormatHeading(line.Substring(3), 2);
                }
                else if (line.StartsWith("#"))
                {
                    FormatHeading(line.Substring(2), 1);
                }
                else if (line.StartsWith("```"))
                {
                    FormatCode(line);
                }
                else*/ if (line.Contains("**"))
                {
                    int startIndex = _richTextBox.TextLength;
                    _richTextBox.AppendText(line.Replace("**", "  ").Replace("~~", "  "));//.Replace("*", " "));
                    FormatBold(startIndex, line);
                    FormatItalic(startIndex, line);
                    FormatStrikeout(startIndex, line);
                }
                /*else if (line.StartsWith("* "))
                {
                    FormatListItem(line.Substring(2), "• ");
                }
                else if (line.Contains("> "))
                {
                    FormatBlockquote(line.Substring(2));
                }*/
                else
                {
                    FormatParagraph(line);
                }
            }
        }

        private static void FormatHeading(string text, int level)
        {
            _richTextBox.SelectionFont = new Font(_richTextBox.Font.FontFamily, 18 - (level * 2), FontStyle.Bold);
            _richTextBox.AppendText(text + Environment.NewLine);
        }

        private static void FormatParagraph(string text)
        {
            FormatInlineStyles(text + Environment.NewLine);
        }

        private static void FormatListItem(string text, string separator)
        {
            FormatInlineStyles(separator + text + Environment.NewLine);
        }

        private static void FormatBlockquote(string text)
        {
            _richTextBox.SelectionColor = Color.Gray;
            _richTextBox.SelectionFont = new Font(_richTextBox.Font, FontStyle.Italic);
            FormatInlineStyles("  > " + text + Environment.NewLine);
        }

        private static void FormatInlineStyles(string text)
        {
            int startIndex = _richTextBox.TextLength;
            _richTextBox.AppendText(text);
            FormatBold(startIndex, text);
            FormatHighlighted(startIndex, text);
        }

        private static void FormatBold(int startIndex, string text)
        {
            string pattern = @"\*\*(.*?)\*\*";
            foreach (Match match in Regex.Matches(text, pattern))
            {
                _richTextBox.Select(startIndex + match.Index, match.Length);
                _richTextBox.SelectionFont = new Font(_richTextBox.SelectionFont, FontStyle.Bold);
                _richTextBox.Select(_richTextBox.TextLength, 0);                
            }
        }

        private static void FormatItalic(int startIndex, string text)
        {
            string pattern = @"\*(.*?)\*";
            foreach (Match match in Regex.Matches(text, pattern))
            {
                _richTextBox.Select(startIndex + match.Index, match.Length);
                _richTextBox.SelectionFont = new Font(_richTextBox.SelectionFont, FontStyle.Italic);
                _richTextBox.Select(_richTextBox.TextLength, 0);
            }
        }

        private static void FormatStrikeout(int startIndex, string text)
        {
            string pattern = @"~~(.*?)~~";
            foreach (Match match in Regex.Matches(text, pattern))
            {
                _richTextBox.Select(startIndex + match.Index, match.Length);
                _richTextBox.SelectionFont = new Font(_richTextBox.SelectionFont, FontStyle.Strikeout);
                _richTextBox.Select(_richTextBox.TextLength, 0);
            }
        }

        private static void FormatHighlighted(int startIndex, string text)
        {
            string pattern = @"\+(.*?)\+";
            foreach (Match match in Regex.Matches(text, pattern))
            {
                _richTextBox.Select(startIndex + match.Index, match.Length);
                _richTextBox.SelectionBackColor = Color.Yellow;
                _richTextBox.Select(_richTextBox.TextLength, 0);
            }
        }

        private static void FormatCode(string text)
        {
            _richTextBox.SelectionColor = Color.DarkMagenta;
            _richTextBox.SelectionFont = new Font(_richTextBox.Font, FontStyle.Italic);
            FormatInlineStyles(text + Environment.NewLine);
        }
    }
}
