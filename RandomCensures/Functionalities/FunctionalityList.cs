using System;
using System.Collections.Generic;

namespace RandomCensures.Functionalities
{
    public class FunctionalityList : List<Functionality>
    {
        public T FindByType<T>() where T : Functionality
        {
            foreach (Functionality item in this)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }

            return null;
        }

        public Functionality FindMatch(Message message)
        {
            foreach (Functionality item in this)
            {
                if (item.IsMatch(message))
                {
                    return item;
                }
            }

            return null;
        }

        public static FunctionalityList CreateAllFunctionalities(Bot bot)
        {
            FunctionalityList functionalities = new FunctionalityList();
            functionalities.Add(new FloodFunctionality(bot));
            functionalities.Add(new LinkFunctionality(bot));
            functionalities.Add(new InsultFunctionality(bot));
            functionalities.Add(new CommandFunctionality(bot));
            return functionalities;
        }
    }
}