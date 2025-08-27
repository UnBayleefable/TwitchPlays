using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchPlaysController.Models
{

    public class BindingConfig
    {
        public List<KeyMapPreset> Presets { get; set; } = new();
    }


    public class KeyMapPreset
    {
        public string Name { get; set; }
        public List<KeyBinding> Bindings { get; set; } = new();
    }

    public class KeyBinding
    {
        public string Command { get; set; }
        public string Action { get; set; }
        public ushort Key { get; set; }
        public int Duration { get; set; }
        public Point MouseMovement { get; set; }
    }
}
