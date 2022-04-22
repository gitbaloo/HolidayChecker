using System.Collections.Generic;

namespace MarioPizza;

public class Menu : IMenu
{
  public ICollection<IPizza> AllPizzas { get; set; }
  
  public ICollection<string> FindPizza(IList<string> mustHaveIngredients, IList<string> wontHaveIngredients)
  {
    throw new NotImplementedException();
  }
}
