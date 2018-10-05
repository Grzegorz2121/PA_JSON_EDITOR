﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;

namespace PA_JSON_EDITOR
{
    class DataContainerComplex : DataContainer, IDataComplex
    {
        //For complex containers
        public int ComplexAmount = 0;
        public Dictionary<string, IDataContainer> ComplexElements = new Dictionary<string, IDataContainer>();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public DataContainerComplex(KeyValuePair<string, JToken> InTokenPair, int InParentTier, string InParentName) : base(InTokenPair, InParentTier, InParentName)
        {
            ContainerType = DataContainerType.Complex;

            //If the Token is complex it has to be a JObject with dictonary of next tokens
            foreach (KeyValuePair<string, JToken> Pair in (JObject)InTokenPair.Value)
            {
                //Creates a new token if token wasnt found on the list already, updates the token when it exists
               /* if (ComplexElements.ContainsKey(Pair.Key))
                {
                    ComplexElements[Pair.Key].Update(Pair, Name);
                }
                else
                {*/
                    ComplexElements.Add(Pair.Key, CreateNewDataContainer(Pair, Tier, Name));
               // }
            }
        }

        public override JToken GetTheData()
        {
            JObject OutputArray = new JObject();
            foreach (KeyValuePair<string, IDataContainer> ChildContainer in ComplexElements)
            {
                OutputArray.Add(ChildContainer.Key, ChildContainer.Value.GetTheData());
            }
            return (JToken)OutputArray;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public string[] GetItemNames()
        {
            return ComplexElements.Keys.ToArray<string>();
        }

        public int GetAmountOfItems()
        {
            return ComplexElements.Count;
        }

        public IDataContainer GetChild(string name)
        {
            return ComplexElements[name];
        }

        public IDataContainer[] GetChilden()
        {
            return ComplexElements.Values.ToArray<IDataContainer>();
        }

        public void AddItem(string name, IDataContainer newItem)
        {
            ComplexElements.Add(name, newItem);
        }

        public void EditItem(string name, IDataContainer newItem)
        {
            ComplexElements[name] = newItem;
        }

        public void DeleteItem(string name)
        {
            ComplexElements.Remove(name);
        }
    }
}