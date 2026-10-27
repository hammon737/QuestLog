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

            Quests.Add(new Quest { Title = "Speak to the innkeeper", Giver = "Innkeeper", Reward = 50 });
            Quests.Add(new Quest { Title = "Buy a lantern", Giver = "Innkeeper", Reward = 10 });

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
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if (Quests.Count > 0)
            {
                int latest = Quests.Count - 1;
                Quests.RemoveAt(latest);
                CountLabel.Text = $"{Quests.Count} Quests";
            }

        }
        
    }
}
