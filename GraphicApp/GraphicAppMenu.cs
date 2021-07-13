using ConsoleMenuComponent;
using ConsoleMenuComponent.Abstractions;
using GraphicApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicApp
{
    public class GraphicAppMenu
    {
        private ConsoleMenu mainMenu;
        private PluginsManager pluginsManager = new PluginsManager();
        private GraphicGroupElement canvas = new GraphicGroupElement() { Identifier = -1 };

        private void ReadElementDataFromConsole(IGraphicPlugin selectedPlugin)
        {
            Console.Clear();
            Console.WriteLine($" - Add {selectedPlugin.Name} - ");
            var reader = selectedPlugin.Reader;
            var element = reader.ReadElement();
            canvas.AddElement(element);
        }

        private int ReadValidElementIdentifier()
        {
            bool validIdentifier = false;
            int identifier = 0;
            do
            {
                Console.Write("Please enter the identifier for the element: ");
                var identifierText = Console.ReadLine();
                validIdentifier = int.TryParse(identifierText, out identifier);
                if (!validIdentifier)
                {
                    Console.WriteLine("PLEASE ENTER A VALID NUMBER");
                }
            } while (!validIdentifier);

            return identifier;
        }

        private  List<BaseMenuItem> GetAddElementMenuItems()
        {
            var items = new List<BaseMenuItem>();
            var plugins = pluginsManager.GetPlugins();
            foreach (var plugin in plugins) 
            {
                var currentItem = new ConsoleMenuItem(plugin.Identifier, 
                                                      plugin.Name, 
                                                      (parent) => 
                                                      {
                                                          ReadElementDataFromConsole(plugin);    
                                                      });
                items.Add(currentItem);
            }
            return items;
        }

        private  void RemoveElement()
        {
            Console.Clear();
            Console.WriteLine(" - Remove Element -");            
            int identifier = ReadValidElementIdentifier();  
            try
            {
                canvas.RemoveElement(identifier);
                Console.WriteLine($"REMOVED ELEMENT {identifier}");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"ERROR!!! {e.Message}");
            }
            Console.ReadLine();

        }

        private  void ModifyElement()
        {
            Console.Clear();
            Console.WriteLine(" - Modify Element -");
            int identifier = ReadValidElementIdentifier();
            try
            {
                var selectedElement = canvas.GetGraphicElement(identifier);
                var plugins = pluginsManager.GetPlugins();
                var selectedPlugin = plugins.Where(plugin => plugin.ElementType == selectedElement.GetType())
                       .SingleOrDefault();

                if (selectedPlugin == null)
                {
                    Console.WriteLine("Unexpected error encountered. ");
                    return;
                }
                selectedElement.Draw();
                if (selectedPlugin.Reader != null)
                {
                    selectedPlugin.Reader.UpdateElement(selectedElement);
                }

                Console.WriteLine($"UPDATED ELEMENT {identifier}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine($"ERROR!!! Element cannot be found.");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"ERROR!!! Element cannot be found.");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR!!! Unexpected error encountered. Please make sure that the modules that you are loading are valid");
            }
            Console.ReadLine();
        }

        private  void ViewCanvas()
        {
            Console.Clear();
            canvas.Draw();

            Console.ReadLine();
        }
        public void Initialize(string baseDirectory)
        {
            pluginsManager.LoadPlugins(baseDirectory);
            var mainMenuItems = new List<BaseMenuItem> {
                                                            new ConsoleMenu(1, "Add element", GetAddElementMenuItems()),
                                                            new ConsoleMenuItem(2, "Remove", (parent) => {
                                                                                                        RemoveElement();
                                                            }),
                                                            new ConsoleMenuItem(3, "Modify", (parent) => {
                                                                ModifyElement();
                                                            }),
                                                            new ConsoleMenuItem(4, "View Canvas", (parent) =>{
                                                                ViewCanvas();
                                                            })
            };

            mainMenu = new ConsoleMenu(mainMenuItems);
            
        }

        public void Run()
        {
            if (mainMenu != null)
            {
                mainMenu.Execute(this);
            }
            Console.WriteLine("Closing application ...");
        }
    }
}
