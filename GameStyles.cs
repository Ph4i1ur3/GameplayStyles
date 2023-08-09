using Oxide.Core;
using Oxide.Core.Libraries.Covalence;
using static ConVar.Admin;

namespace Oxide.Plugins
{
    internal class GameStyles : CovalencePlugin
    {
        #region Fields

        private List<ILifestyle> LifestyleList = new List<ILifestyle>();
        private List<IPlaystyle> PlaystyleList = new List<IPlaystyle>();
        private DataFileSystem dataFile;

        #region API
        public interface ILifestyle
        {
            string getName();
            string getDescription();
            string getIcon();

        }

        public interface IPlaystyle
        {
            string getName();
            string getDescription();
            string getIcon();
        }

        public void RegisterPlaystyle(IPlaystyle playstyle)
        {
            if (playstyle != null && !PlaystyleList.Contains(playstyle)) {
                PlaystyleList.Add(playstyle);
            }
        }

        public void RegisterLifestyle(ILifestyle lifestyle)
        {
            if (lifestyle != null && !LifestyleList.Contains(lifestyle)) {
                LifestyleList.Add(lifestyle);
            }
        }
        #endregion

        void OnServerInitialized()
        {

        }

        void OnPlayerConnected(IPlayer player)
        {
            if (Interface.Oxide.DataFileSystem.ExistsDatafile("Styles/" + player.Id.ToString()))
            {

            }
        }

        private class PlayerInfo
        {
            public string Id;
            public string Name;
            public IPlaystyle Playstyle;
            public ILifestyle Lifestyle;

            public PlayerInfo(IPlayer player, IPlaystyle playstyle, ILifestyle lifestyle)
            {
                Id = player.Id;
                Name = player.Name;
                Playstyle = playstyle;
                Lifestyle = lifestyle;
            }
        }

        private void Init()
        {
            dataFile = new DataFileSystem($"{Interface.Oxide.DataDirectory}\\GameStyles");
        }

        private PlayerInfo LoadPlayerInfo(string playerId)
        {
            return dataFile.ReadObject<PlayerInfo>($"playerInfo_{playerId}");
        }

        private void SavePlayerInfo(string playerId, PlayerInfo playerInfo)
        {
            dataFile.WriteObject<PlayerInfo>($"playerInfo_{playerId}", playerInfo);
        }
    }
}
