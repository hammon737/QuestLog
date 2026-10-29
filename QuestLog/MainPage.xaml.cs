namespace QuestLog
{
    using System.Collections.ObjectModel;
    using System.Text.Json;

    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Quest> Quests { get; } = new ObservableCollection<Quest>();

        Quest[] pool =
        {

        };

        int next = 0;

        public MainPage()
        {
            InitializeComponent();
            
            BindingContext = this;

            Load();


            if (Quests.Count > 0)
            {
                FinishButton.IsEnabled = true;
            }
            else
            {
                FinishButton.IsEnabled = false;
            }


        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            string reward = QuestReward.Text;

            if (int.TryParse(reward, out int result))
            {
            }
            else
            {
                return;
            }


            Quests.Add(new Quest { Title = QuestName.Text, Giver = QuestGiver.Text, Reward = result});

            next++;
            if (next >= pool.Length)
                next = 0;
            CountLabel.Text = $"{Quests.Count} Quests";

            Save();
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if (Quests.Count > 0)
            {
                int latest = Quests.Count - 1;
                Quests.RemoveAt(latest);
                CountLabel.Text = $"{Quests.Count} Quests";
            }

            Save();
        }

        string path = Path.Combine(FileSystem.AppDataDirectory,"questlog.json");

        void Save()
        {
            string json = JsonSerializer.Serialize(Quests);
            File.WriteAllText(path, json);
        }

        void Load()
        {
            if (!File.Exists(path))
            {
                return;
            }
            string json = File.ReadAllText(path);
            List<Quest> saved = JsonSerializer.Deserialize<List<Quest>>(json);
            foreach(Quest q in saved)
            {
                Quests.Add(q);
            }

        }
        
    }
}
