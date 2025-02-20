using ConsoleUtils.Rendering;

namespace ConsoleUtils.Interaction {
    public class MultipleChoiceOption {
        public static int AskOption(List<String> options, string prompt) {
            TextRenderer.RenderList(options);
            try {
                return Convert.ToInt32(TextRenderer.GetUserResponse(prompt)) - 1;
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
    }
}
