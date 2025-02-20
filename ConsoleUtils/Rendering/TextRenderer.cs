
namespace ConsoleUtils.Rendering {
    public class TextRenderer {
        public static void RenderList(List<String> list) {
            for (int i = 0; i < list.Count; i++) {
                Console.Write($"({i + 1}) - {list[i]} | ");
            }    
        }    

        public static string GetUserResponse(string prompt) {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
