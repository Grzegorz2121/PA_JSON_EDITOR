using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static Pa_Looker_2.Folder_tools;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using static Pa_Looker_2.ICallback;

namespace Pa_Looker_2
{

    ////////////////////////////////////////////////////////////////////////////////////////////////
    static class Tools
    {
        public static bool IsPrimitive(KeyValuePair<string, JToken> input_token)
        {
            if (input_token.Value.Type == JTokenType.Boolean || input_token.Value.Type == JTokenType.Float || input_token.Value.Type == JTokenType.String || input_token.Value.Type == JTokenType.Integer)
                return true;
            else
                return false;
        }

        public static bool IsArray(KeyValuePair<string, JToken> input_token)
        {
            if (input_token.Value.Type == JTokenType.Array)
                return true;
            else
                return false;
        }

        public static bool IsObject(KeyValuePair<string, JToken> input_token)
        {
            if (input_token.Value.Type == JTokenType.Object)
                return true;
            else
                return false;
        }
    }





    class GUI_node_folder
    {
        ListBox list_box;

        string path;
        Form form;
        Point location;

        GUI_node_folder child_folder;
        GUI_node_file child_file;

        public GUI_node_folder(Form in_form, string in_path, Point in_loc)
        {
            form = in_form;
            path = in_path;
            location = in_loc;

            if (Is_folder_there(path))
            {
                list_box = new ListBox();
                list_box.Location = location;
                string[] folders = ExtractFolders(path);

                foreach(string folder in folders)
                {
                    list_box.Items.Add(folder);
                }
                list_box.SelectedIndexChanged += new EventHandler(list_box_index_change);
                form.Controls.Add(list_box);
            }
            if (Is_json_there(path))
            {
                child_file = new GUI_node_file(form, path, new Point(location.X, location.Y + 130));
            }

        }

        void list_box_index_change(object sender, EventArgs e)
        {
            if(child_folder!=null)
            {
                child_folder.Dispose();
                child_folder = null;
            }
            child_folder = new GUI_node_folder(form, Lenghten_path(path, (string)list_box.SelectedItem+"\\"), new Point(location.X+130, location.Y));
        }

        public string[] ExtractFolders(string path)
        {
           return Shorten_path(path,Directory.GetDirectories(path));
        }

        public void Dispose()
        {
            form.Controls.Remove(list_box);
            if (child_folder != null)
            {
                child_folder.Dispose();
                child_folder = null;
            }
            if (child_file != null)
            {
                child_file.Dispose();
                child_file = null;
            }
        }

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    class GUI_node_file
    {
        ListBox list_box;

        string path;
        Form form;
        Point location;

        GUI_node_json child_json;

        public GUI_node_file(Form in_form, string in_path, Point in_loc)
        {
            form = in_form;
            path = in_path;
            location = in_loc;

            list_box = new ListBox();
            list_box.Location = location;
            string[] jsons = ExtractJsons(path);

            foreach (string json in jsons)
            {
                list_box.Items.Add(json);
            }
            list_box.SelectedIndexChanged += new EventHandler(list_box_index_change);
            list_box.MouseDoubleClick += new MouseEventHandler(list_box_right_click);
            form.Controls.Add(list_box);
        }

        void list_box_right_click(object sender, EventArgs e)
        {
            if(child_json!=null)
            {
                child_json.Dispose();
                child_json = null;
            }
            list_box.ClearSelected();
        }

        void list_box_index_change(object sender, EventArgs e)
        {
            
            if (child_json != null)
            {
                child_json.Dispose();
                child_json = null;
            }
            if(list_box.SelectedItem!=null)
            {
                child_json = new GUI_node_json(form, Lenghten_path(path, (string)list_box.SelectedItem), new Point(30, 360));
            }
        }

        public string[] ExtractJsons(string path)
        {
            return Shorten_path(path, Directory.GetFiles(path,"*.json",SearchOption.TopDirectoryOnly));
        }

        public void Dispose()
        {
            form.Controls.Remove(list_box);
            if (child_json != null)
            {
                child_json.Dispose();
                child_json = null;
            }
        }

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

 
    class GUI_complex_node
    {
        //GUI elements
        ListBox list_box;
        Label label;

        //Form reference for GUI
        Form form;
        Point location;

        //Callback ref to parent (save procedure)
        ICallback parent;

        //Input data (for processing)
        KeyValuePair<string, JToken> json_element;

        List<JToken> json_values;

        GUI_complex_node child_complex;
        //GUI_primitive_node child_primitive;


        public GUI_complex_node(Form in_form, KeyValuePair<string, JToken> in_pair, object in_parent, Point in_loc)
        {
            parent = (ICallback)in_parent;
            form = in_form;
            location = in_loc;
            json_element = in_pair;

            label = new Label();
            label.Location = new Point(location.X, location.Y - 20);
            list_box = new ListBox();
            list_box.Location = location;

            foreach(KeyValuePair<string, JToken> token in json_element.Value as JObject)
            {
                list_box.Items.Add(token.Key);
            }

            list_box.SelectedIndexChanged += new EventHandler(list_box_index_change);

            //Display the list and label
            form.Controls.Add(list_box);
            form.Controls.Add(label);
        }

        void list_box_index_change(object sender, EventArgs e)
        {

        }

        public void Callback(string child_key, JToken child_token)
        {

        }

      /*  public string[] ExtractProperties(JObject job)
        {

        }*/

        public void Dispose()
        {
            form.Controls.Remove(label);
            form.Controls.Remove(list_box);
        }
    }

    
    ////////////////////////////////////////////////////////////////////////////////////////////////

    class GUI_node_json : ICallback
    {
        Button save_button;
        ListBox list_box;
        Label label;

        string path;
        Form form;
        Point location;

        JObject jobject;

       // GUI_complex_node node;

        GUI_node_property child_property;
        GUI_node_editbox child_editbox;
        GUI_node_editarray child_editarray;

        public GUI_node_json(Form in_form, string in_path, Point in_loc)
        {
            form = in_form;
            path = in_path;
            location = in_loc;

            save_button = new Button();
            save_button.Location = new Point(location.X, location.Y-50);
            save_button.Text = "Commit";
            save_button.Click += new EventHandler(save_button_click);
            label = new Label();
            label.Location = new Point(location.X, location.Y - 20);
            list_box = new ListBox();
            list_box.Location = location;
            string[] jsons = ExtractValues(path);

            foreach (string json in jsons)
            {
                list_box.Items.Add(json);
            }
            list_box.SelectedIndexChanged += new EventHandler(list_box_index_change);
            form.Controls.Add(list_box);
            form.Controls.Add(label);
            form.Controls.Add(save_button);
        }

        void list_box_index_change(object sender, EventArgs e)
        {
            if (child_editarray != null)
            {
                child_editarray.Dispose();
                child_editarray = null;
            }
            if (child_property != null)
            {
                child_property.Dispose();
                child_property = null;
            }
            if (child_editbox != null)
            {
                child_editbox.Dispose();
                child_editbox = null;
            }
            string key = (string)list_box.SelectedItem;

            //Console.WriteLine(jobject[key].Type);
            var temp = jobject[key] as JObject;

            List<JObject> list = new List<JObject>();

            foreach(JToken t in jobject.Children())
            {
                list.Add(t as JObject);
            }

            List<JObject> list2 = new List<JObject>();

            foreach (JToken t in jobject[key])
            {
                list2.Add(t as JObject);
            }

            label.Text = jobject[key].Type.ToString();
            // THAT IS TOKEN TYPE! good for labels, terrible for converting to objects!

            //var temp = jobject[key];

            if (jobject[key].Type == JTokenType.Object)
            {
                child_property = new GUI_node_property(form, new KeyValuePair<string, JToken>(key, jobject[key]), this, new Point(location.X + 130, location.Y));
                //Console.WriteLine("COMPLEX");
                // child_property = new GUI_node_property(form, new KeyValuePair<string, JToken>(key, jobject[key]), this, new Point(location.X + 130, location.Y));
            }
            if (jobject[key].Type == JTokenType.Array)
            {
                child_editarray = new GUI_node_editarray(form, new KeyValuePair<string, JToken>(key, jobject[key]), this, new Point(location.X + 130, location.Y));
                //Console.WriteLine("COMPLEX");
                // child_editarray = new GUI_node_editarray(form, new KeyValuePair<string, JToken>(key, jobject[key]), this, new Point(location.X + 130, location.Y));
                // child_property = new GUI_node_property(form, new KeyValuePair<string, JToken>(key, jobject[key]), this, new Point(location.X + 130, location.Y));
            }
            if (jobject[key].Type == JTokenType.Float || jobject[key].Type == JTokenType.Boolean || jobject[key].Type == JTokenType.Integer || jobject[key].Type == JTokenType.String)
            {
                //Console.WriteLine("PRIMITIVE");
                 child_editbox = new GUI_node_editbox(form, new KeyValuePair<string, JToken>(key, jobject[key]), this, new Point(location.X, location.Y + 130));
                //Console.WriteLine(jobject[key]);
            }
        }

        public void save_button_click(object sender, EventArgs e)
        {
            File.Create(path).Close();
            StreamWriter sw = new StreamWriter(path);
            sw.Write(JsonConvert.SerializeObject(jobject));
            sw.Close();
        }

        public void Callback(string key, JToken token)
        {
            if (token != null)
            {
                jobject[key].Replace(token);
            }
            else
            {
                jobject.Remove(key);
            }
        }

        public string[] ExtractValues(string path)
        {
            string[] keys;
            List<string> key_list = new List<string>();
            StreamReader sr = new StreamReader(path);
            jobject = JsonConvert.DeserializeObject(sr.ReadToEnd()) as JObject;
            
            foreach(KeyValuePair<string, JToken> jtoken in jobject)
            {
                Console.WriteLine(jtoken.Value.Type);
                key_list.Add(jtoken.Key);
            }
            keys = key_list.ToArray();
            sr.Close();
            return keys;
        }

        public void Dispose()
        {
            form.Controls.Remove(label);
            form.Controls.Remove(list_box);
            form.Controls.Remove(save_button);
            if (child_property != null)
            {
                child_property.Dispose();
                child_property = null;
            }
            if (child_editbox != null)
            {
                child_editbox.Dispose();
                child_editbox = null;
            }
            if (child_editarray != null)
            {
                child_editarray.Dispose();
                child_editarray = null;
            }
        }

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    class GUI_node_property : ICallback 
    {
        ListBox list_box;
        Label label;

        Form form;
        Point location;

        ICallback parent;

        string key;
        JToken token;

        object obj;
        JObject jobject_part;

        GUI_node_property child_property;
        GUI_node_editbox child_editbox;
        GUI_node_editarray child_editarray;

        public GUI_node_property(Form in_form, KeyValuePair<string, JToken> in_pair, object in_parent, Point in_loc)
        {
            parent = (ICallback)in_parent;

            form = in_form;
           // property = in_property;
            location = in_loc;

            key = in_pair.Key;
            token = in_pair.Value;

            //jobject_part = token as JObject;
            jobject_part = new JObject(token as JObject);

            label = new Label();
            label.Location = new Point(location.X, location.Y - 20);
            list_box = new ListBox();
            list_box.Location = location;
            string[] jsons = ExtractProperties(jobject_part);

            foreach (string json in jsons)
            {
                list_box.Items.Add(json);
            }
            list_box.SelectedIndexChanged += new EventHandler(list_box_index_change);

            form.Controls.Add(list_box);
            form.Controls.Add(label);
        }

        void list_box_index_change(object sender, EventArgs e)
        {
            if (child_property != null)
            {
                child_property.Dispose();
                child_property = null;
            }
            if (child_editbox != null)
            {
                child_editbox.Dispose();
                child_editbox = null;
            }
            if (child_editarray != null)
            {
                child_editarray.Dispose();
                child_editarray = null;
            }
            string selected_key = (string)list_box.SelectedItem;

            //Console.WriteLine(jobject[key].Type);

            label.Text = jobject_part[selected_key].Type.ToString();
            // THAT IS TOKEN TYPE! good for labels, terrible for converting to objects!

            var temp = jobject_part[selected_key];

            if (jobject_part[selected_key].Type == JTokenType.Object)
            {
                //Console.WriteLine("COMPLEX");
                child_property = new GUI_node_property(form,new KeyValuePair<string, JToken>(selected_key, jobject_part[selected_key]), this, new Point(location.X + 130, location.Y));
            }
            if (jobject_part[selected_key].Type == JTokenType.Array)
            {
                //Console.WriteLine("COMPLEX");
               // child_property = new GUI_node_property(form, new KeyValuePair<string, JToken>(selected_key, jobject_part[selected_key]), this, new Point(location.X + 130, location.Y));
                child_editarray = new GUI_node_editarray(form, new KeyValuePair<string, JToken>(selected_key, jobject_part[selected_key]), this, new Point(location.X + 130, location.Y));
            }
            if (jobject_part[selected_key].Type == JTokenType.Float || jobject_part[selected_key].Type == JTokenType.Boolean || jobject_part[selected_key].Type == JTokenType.Integer || jobject_part[selected_key].Type == JTokenType.String)
            {
                //Console.WriteLine("PRIMITIVE");
                child_editbox = new GUI_node_editbox(form, new KeyValuePair<string, JToken>(selected_key, jobject_part[selected_key]),this , new Point(location.X, location.Y + 130));
                //Console.WriteLine(jobject[key]);
            }
        }

        public void Callback(string child_key, JToken child_token)
        {
            /*
            jobject_part[child_key].Replace(child_token);

            token = jobject_part;*/
            /*
            if (parent != null)
            {
                parent.Callback(key, token);
            }*/
            if (token != null)
            {
                jobject_part[child_key].Replace(child_token);

                token = jobject_part;
            }
            else
            {
                jobject_part[child_key].Remove();

                token = jobject_part;
            }
            parent.Callback(key, token);
        }

        public string[] ExtractProperties(JObject job)
        {
            string[] keys;
            List<string> key_list = new List<string>();
            //JObject jobject = property as JObject;d
            foreach (KeyValuePair<string, JToken> jtoken in job)
            {
                key_list.Add(jtoken.Key);
            }
            keys = key_list.ToArray();
            return keys;
        }

        public void Dispose()
        {
            form.Controls.Remove(label);
            form.Controls.Remove(list_box);
            if (child_property != null)
            {
                child_property.Dispose();
                child_property = null;
            }
            if (child_editbox != null)
            {
                child_editbox.Dispose();
                child_editbox = null;
            }
            if (child_editarray != null)
            {
                child_editarray.Dispose();
                child_editarray = null;
            }
        }

    }
    ////////////////////////////////////////////////////////////////////////////////////////////////

    class GUI_node_editbox
    {
        TextBox textBox;
        Button button;

        Form form;
        Point location;

        ICallback parent;

        public string key;
        public object primitive;
        public Type primitive_type;
        public KeyValuePair<string, JToken> token_pair;
        public JToken jtoken;

        public GUI_node_editbox(Form in_form, KeyValuePair<string, JToken> in_primitive_token, object in_parent , Point in_loc)
        {
            parent = (ICallback)in_parent;

            form = in_form;
            location = in_loc;

            token_pair = in_primitive_token;
            key = in_primitive_token.Key;
            jtoken = in_primitive_token.Value;
            primitive = jtoken.ToObject(typeof(object));
            primitive_type = primitive.GetType();


            button = new Button();
            button.Text = "Save";
            button.Location = new Point(location.X, location.Y + 30);
            button.Click += new EventHandler(save_button_click);
            textBox = new TextBox();
            textBox.Location = location;
            textBox.Text = primitive.ToString();
            form.Controls.Add(textBox);
            form.Controls.Add(button);
        }
       



        public void save_button_click(object sender, EventArgs e)
        {
            JToken new_token = jtoken;

            if(textBox.Text == String.Empty)
            {
                parent.Callback(key, null);
                return;
            }

            switch(primitive_type.Name.ToString())
            {
                case "String":
                    new_token = new JValue(Convert.ToString(textBox.Text));
                    break;
                case "Int64":
                    new_token = new JValue(Convert.ToInt64(textBox.Text));
                    break;
                case "Double":
                    new_token = new JValue(Convert.ToDouble(textBox.Text));
                    break;
                case "Boolean":
                    new_token = new JValue(Convert.ToBoolean(textBox.Text));
                    break;
            }

            parent.Callback(key, new_token);

            Console.WriteLine(new_token.Type);
            //JTOKEN IS A JVALUE
        }

        public void Dispose()
        {
            form.Controls.Remove(textBox);
            form.Controls.Remove(button);
        }

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class GUI_node_editarray : ICallback
    {
        Label label;
        Form form;
        Point location;

        ListBox listBox;

        GUI_node_editbox child_editbox;
        //for primitive arrays
        GUI_node_property child_property;
        //for object arrays
        GUI_node_editarray child_editarray;
        //for jagged arrays

        ICallback parent;

        public bool IsPrimitive = false;
        public bool IsJagged = false;
        public bool IsObject = false;

        public KeyValuePair<string, JToken> token_pair;

        public List<JToken> jTokens;

        public GUI_node_editarray(Form in_form, KeyValuePair<string, JToken> in_token_pair, object in_parent, Point in_loc)
        {
            token_pair = in_token_pair;
            parent = (ICallback)in_parent;

            form = in_form;
            location = in_loc;

            listBox = new ListBox();
            listBox.Location = location;
            listBox.SelectedIndexChanged += new EventHandler(list_box_index_change);

            label = new Label();
            label.Location = new Point(location.X, location.Y - 20);
            jTokens = new List<JToken>();

            foreach(JToken t in token_pair.Value)
            {
                jTokens.Add(t);
            }

            if (IsArrayPrimitive(token_pair.Value))
            {
                IsPrimitive = true;
                IsJagged = false;
                IsObject = false;
                label.Text = "Primitive Array";
                foreach (JToken ob in jTokens)
                {
                    listBox.Items.Add(ob);
                }
            }
            if (IsArrayJagged(token_pair.Value))
            {
                IsPrimitive = false;
                IsJagged = true;
                IsObject = false;
                label.Text = "Jagged Array";
                foreach (JToken ob in jTokens)
                {
                    listBox.Items.Add(ob);
                }
            }
            if (IsArrayComplex(token_pair.Value))
            {
                IsPrimitive = false;
                IsJagged = false;
                IsObject = true;
                label.Text = "Complex Array";
                foreach (JToken ob in jTokens)
                {
                    listBox.Items.Add(ob);
                }
            }

            form.Controls.Add(listBox);
            form.Controls.Add(label);
            
        }

        public void Callback(string k, JToken t)
        {
            JArray jarray;
            /*
            if (parent != null)
            {
                parent.Callback(token_pair.Key, jarray);
            }*/
            if (t != null)
            {
                jTokens[Convert.ToInt32(k)] = t;
                listBox.Items.Clear();
                foreach (JToken ob in jTokens)
                {
                    listBox.Items.Add(ob);
                }

                jarray = new JArray(jTokens);
            }
            else
            {
                jTokens.RemoveAt(Convert.ToInt32(k));
                listBox.Items.Clear();
                foreach (JToken ob in jTokens)
                {
                    listBox.Items.Add(ob);
                }

                jarray = new JArray(jTokens);
            }
            parent.Callback(token_pair.Key, jarray);
        }


        public bool IsArrayPrimitive(JToken in_token_arr)
        {
            List<JToken> temp = new List<JToken>();
            foreach(JToken ob in in_token_arr.Values<object>())
            {
                temp.Add(ob);
            }
            if(temp[0].Type==JTokenType.Boolean || temp[0].Type == JTokenType.Float || temp[0].Type == JTokenType.String || temp[0].Type == JTokenType.Integer)
            {
                return true;
            }
            return false;
        }

        public bool IsArrayJagged(JToken in_token_arr)
        {
            List<JToken> temp = new List<JToken>();
            foreach (JToken ob in in_token_arr.Values<object>())
            {
                temp.Add(ob);
            }
            if (temp[0].Type == JTokenType.Array)
            {
                return true;
            }
            return false;
        }

        public bool IsArrayComplex(JToken in_token_arr)
        {
            List<JToken> temp = new List<JToken>();
            foreach (JToken ob in in_token_arr.Values<object>())
            {
                temp.Add(ob);
            }
            if (temp[0].Type == JTokenType.Object)
            {
                return true;
            }
            return false;
        }

        void list_box_index_change(object sender, EventArgs e)
        {

            if(IsPrimitive)
            {
                if (child_editbox != null)
                {
                    child_editbox.Dispose();
                    child_editbox = null;
                }
                int selected_key = listBox.SelectedIndex;

                label.Text = jTokens[selected_key].Type.ToString();
                // THAT IS TOKEN TYPE! good for labels, terrible for converting to objects!

                child_editbox = new GUI_node_editbox(form, new KeyValuePair<string, JToken>(selected_key.ToString(), jTokens[selected_key]), this, new Point(location.X, location.Y + 130));
            }

            if (IsJagged)
            {
                if (child_editarray != null)
                {
                    child_editarray.Dispose();
                    child_editarray = null;
                }
                int selected_key = listBox.SelectedIndex;

                label.Text = jTokens[selected_key].Type.ToString();
                // THAT IS TOKEN TYPE! good for labels, terrible for converting to objects!

                child_editarray = new GUI_node_editarray(form, new KeyValuePair<string, JToken>(selected_key.ToString(), jTokens[selected_key]), this, new Point(location.X, location.Y + 130));
            }
            
            if (IsObject && !IsPrimitive && !IsJagged)
            {
                if (child_property != null)
                {
                    child_property.Dispose();
                    child_property = null;
                }
                int selected_key = listBox.SelectedIndex;

                label.Text = jTokens[selected_key].Type.ToString();
                // THAT IS TOKEN TYPE! good for labels, terrible for converting to objects!

                child_property = new GUI_node_property(form, new KeyValuePair<string, JToken>(selected_key.ToString(), jTokens[selected_key]), this, new Point(location.X, location.Y + 130));
            }

        }

        public void save_button_click(object sender, EventArgs e)
        {
        }

        public void Dispose()
        {
            if (child_editbox != null)
            {
                child_editbox.Dispose();
                child_editbox = null;
            }
            if (child_editarray != null)
            {
                child_editarray.Dispose();
                child_editarray = null;
            }
            if (child_property != null)
            {
                child_property.Dispose();
                child_property = null;
            }
            form.Controls.Remove(label);
            form.Controls.Remove(listBox);
        }

    }
}
