using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace _9._07
{
    public partial class MainWindow : Window
    {
        public Character SelectedCharacter { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadCharacters();
        }

        public async void LoadCharacters()
        {
            string apiUrl = "https://rickandmortyapi.com/api/character";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    CharacterResponse characterResponse = JsonConvert.DeserializeObject<CharacterResponse>(responseBody);

                    CharactersListBox.ItemsSource = characterResponse.Results;
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Request error: {e.Message}");
                }
            }
        }

        private void CharactersListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedCharacter = (Character)CharactersListBox.SelectedItem;
            if (SelectedCharacter != null)
            {
                CharacterImage.Source = new BitmapImage(new Uri(SelectedCharacter.Image));
                CharacterName.Text = SelectedCharacter.Name;
                CharacterStatus.Text = "Status: " + SelectedCharacter.Status;
                CharacterSpecies.Text = "Species: " + SelectedCharacter.Species;
                CharacterGender.Text = "Gender: " + SelectedCharacter.Gender;
                CharacterOrigin.Text = "Origin: " + SelectedCharacter.Origin.Name;
                CharacterLocation.Text = "Location: " + SelectedCharacter.Location.Name;
            }
        }
    }

    public class Character
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("species")]
        public string? Species { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [JsonProperty("origin")]
        public Origin? Origin { get; set; }

        [JsonProperty("location")]
        public Location? Location { get; set; }

        [JsonProperty("image")]
        public string? Image { get; set; }

        [JsonProperty("episode")]
        public List<string>? Episode { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }

    public class Origin
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }

    public class Location
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }

    public class CharacterResponse
    {
        [JsonProperty("results")]
        public List<Character>? Results { get; set; }
    }
}
