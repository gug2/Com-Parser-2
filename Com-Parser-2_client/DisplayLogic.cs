using System.Collections.Generic;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    public class DisplayLogic
    {
        private static DisplayLogic Instance;

        public PacketFormat SelectedFormat { private set; get; }

        private readonly Dictionary<string, PacketFormat> LoadedFormats = new Dictionary<string, PacketFormat>();
        private readonly Form parent;
        private readonly Dictionary<string, ChartDisplayForm> Charts = new Dictionary<string, ChartDisplayForm>();
        private string timeDataName;

        private DisplayLogic(Form parent)
        {
            this.parent = parent;
        }

        public static DisplayLogic From(Form parent)
        {
            if (Instance == null)
            {
                Instance = new DisplayLogic(parent);
            }

            return Instance;
        }

#warning сделать возможность перезагрузить уже загруженный скрипт
        public bool TryLoadFormat(string scriptPath)
        {
            if (LoadedFormats.ContainsKey(scriptPath))
            {
                return false;
            }
            LoadedFormats.Add(scriptPath, new PacketFormat(scriptPath));

            SelectedFormat = LoadedFormats[scriptPath];

            if (!SelectedFormat.ScriptCompiled)
            {
                return false;
            }

            Charts.Clear();

            if (parent.MdiParent != null && parent.MdiParent is ClientForm client)
            {
                client.ClearDropDownItems();
                client.ClearCharts();

                DataObject timeData = SelectedFormat.GetOrCreateDataObjects().Find(data => data.Name.Contains("time"));
                timeDataName = timeData.Name;

                foreach (DataObject data in SelectedFormat.GetOrCreateDataObjects())
                {
                    if (data.Plot < 0)
                    {
                        continue;
                    }

                    var chart = client.AddChart(timeData, data);

                    if (chart != null)
                    {
                        Charts.Add(data.Name, chart);
                    }
                }
            }

            return true;
        }

        public void HandlePacket(List<DataObject> datas)
        {
            DataObject timeData = datas.Find(data => data.Name == timeDataName);

            foreach (DataObject data in datas)
            {
                if (data.Plot < 0)
                {
                    continue;
                }

                Charts[data.Name].AddPoints(timeData.Value, data.Value);
            }
        }
    }
}
