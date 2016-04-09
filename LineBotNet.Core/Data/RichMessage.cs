using System.Collections.Generic;
using Newtonsoft.Json;

namespace LineBotNet.Core.Data
{
    public class RichMessage
    {
        [JsonProperty("canvas")]
        public Canvas Canvas { get; set; }

        [JsonProperty("images")]
        public Dictionary<string, Image> Images { get; set; }

        [JsonProperty("actions")]
        public Dictionary<string, Action> Actions { get; set; }

        [JsonProperty("scenes")]
        public Dictionary<string, Scene> Scenes { get; set; }

        public void SetImage(Image image)
        {
            Images = new Dictionary<string, Image>
            {
                ["image1"] = image
            };
        }

        public void AddAction(string actionKey, Action action)
        {
            if (Actions == null)
            {
                Actions = new Dictionary<string, Action>();
            }

            Actions[actionKey] = action;
        }

        public void SetScene(Scene scene)
        {
            Scenes = new Dictionary<string, Scene>
            {
                ["scene1"] = scene
            };
        }
    }

    public class Canvas
    {
        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("initialScene")]
        public string InitialScene { get; set; }
    }

    public class Image
    {
        [JsonProperty("x")]
        public int PositionX { get; set; }

        [JsonProperty("y")]
        public int PositionY { get; set; }

        [JsonProperty("W")]
        public string Width { get; set; }

        [JsonProperty("H")]
        public string Height { get; set; }
    }

    public class Action
    {
        [JsonProperty("type")]
        public string Type => "web";

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("params")]
        public ActionParams Params { get; set; }
    }

    public class ActionParams
    {
        [JsonProperty("linkUri")]
        public string LinkUrl { get; set; }
    }

    public class Scene
    {
        [JsonProperty("draws")]
        public Draws Draws { get; set; }

        [JsonProperty("listeners")]
        public Listener[] Listeners { get; set; }
    }

    public class Draws
    {
        /// <summary>
        /// Use the image ID “image1″.
        /// </summary>
        [JsonProperty("image")]
        public string ImageKey { get; set; }

        [JsonProperty("x")]
        public int PositionX => 0;

        [JsonProperty("y")]
        public int PositionY => 0;

        /// <summary>
        /// Integer value. Any one of 1040, 700, 460, 300, 240. This value must be same as the image width.
        /// </summary>
        [JsonProperty("W")]
        public string Width { get; set; }

        /// <summary>
        /// Integer value. Max value is 2080px.
        /// </summary>
        [JsonProperty("H")]
        public string Height { get; set; }
    }

    public class Listener
    {
        [JsonProperty("type")]
        public string Type => "touch";

        /// <summary>
        /// Array of the rectangle information. [x, y, width, height].
        /// </summary>
        [JsonProperty("params")]
        public int[] Params { get; set; }

        /// <summary>
        /// Action ID string. For example, “openHomepage”.
        /// </summary>
        [JsonProperty("action")]
        public string ActionKey { get; set; }
    }
}