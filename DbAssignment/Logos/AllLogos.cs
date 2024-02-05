

namespace DbAssignment.Logos;

public class AllLogos
{

    private void CenteredText(string text)
    {
        int screenWidth = Console.WindowWidth;
        var lines = text.Split('\n');

        foreach (var line in lines)
        {
            int stringWidth = line.Length;
            int spaces = (screenWidth / 2) + (stringWidth / 2);

            Console.WriteLine(line.PadLeft(spaces));
        }
    }

    public void MainMenuLogo()
    {
        string logo = @"
 _______                      ______                       __       
|_   __ \                    |_   _ `.                    [  |  _   
  | |__) |  _ .--.    .--.     | | `. \   .--.    _ .--.   | | / ]  
  |  ___/  [ `/'`\] / .'`\ \   | |  | | / .'`\ \ [ `/'`\]  | '' <   
 _| |_      | |     | \__. |  _| |_.' / | \__. |  | |      | |`\ \  
|_____|    [___]     '.__.'  |______.'   '.__.'  [___]    [__|  \_] 


                                                          

                                                                    
";
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        CenteredText(logo);
        Console.ResetColor();
    }

    public void ProductsLogo()
    {
        string logo = @"

 _______                            __                      _            
|_   __ \                          |  ]                    / |_          
  | |__) |  _ .--.    .--.     .--.| |   __   _    .---.  `| |-'  .--.   
  |  ___/  [ `/'`\] / .'`\ \ / /'`\' |  [  | | |  / /'`\]  | |   ( (`\]  
 _| |_      | |     | \__. | | \__/  |   | \_/ |, | \__.   | |,   `'.'.  
|_____|    [___]     '.__.'   '.__.;__]  '.__.'_/ '.___.'  \__/  [\__) ) 
                                                                         


                                                          

                                                                    
";
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        CenteredText(logo);
        Console.ResetColor();
    }

    public void UsersLogo()
    {
        string logo = @"



 _____  _____                                   
|_   _||_   _|                                  
  | |    | |    .--.    .---.   _ .--.   .--.   
  | '    ' |   ( (`\]  / /__\\ [ `/'`\] ( (`\]  
   \ \__/ /     `'.'.  | \__.,  | |      `'.'.  
    `.__.'     [\__) )  '.__.' [___]    [\__) ) 
                                                


                                                          

                                                                    
";
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        CenteredText(logo);
        Console.ResetColor();
    }

    public void OrdersLogo()
    {
        string logo = @"


   ___                    __                           
 .'   `.                 |  ]                          
/  .-.  \  _ .--.    .--.| |   .---.   _ .--.   .--.   
| |   | | [ `/'`\] / /'`\' |  / /__\\ [ `/'`\] ( (`\]  
\  `-'  /  | |     | \__/  |  | \__.,  | |      `'.'.  
 `.___.'  [___]     '.__.;__]  '.__.' [___]    [\__) ) 
                                                       


                                                          

                                                                    
";
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        CenteredText(logo);
        Console.ResetColor();
    }
}
