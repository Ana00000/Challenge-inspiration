using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExamplesApp.SRP
{
    class AchievementCollection
    {
        public int UserId { get; }
        private readonly ISet<Achievement> _unlockedAchievements;

        public AchievementCollection(int userId, ISet<Achievement> unlockedAchievements)
        {
            UserId = userId;
            _unlockedAchievements = unlockedAchievements;
        }

        public void UnlockAchievement(Achievement newAchievement)
        {
            if (_unlockedAchievements.Contains(newAchievement)) throw new InvalidOperationException("Achievement " + newAchievement.Name + " is already unlocked!");
            if (!newAchievement.PrerequisitesFulfilled(_unlockedAchievements)) throw new InvalidOperationException("Prerequisite achievement not completed.");
            _unlockedAchievements.Add(newAchievement);
        }
    }

    /// <summary>
    /// Take time to study the code and map how the logic moved from the first example to the second.
    /// 1) Using up to 20 words, describe the responsibility of each class.
    /// 2) Answer the following questions, using concrete code snippets from the examples:
    ///     a) How do the new changes promote better code encapsulation?
    ///     b) How would you further enhance the methods of the Repository class?
    ///     c) Which classes need to change and how if we introduce a category of achievements, where an achievements identity is defined by its name and category (meaning that there can be two achievements with the same name so long as they don't belong to the same category)
    ///     d) Which classes need to change and how if we want to store achievements to an SQL database instead of a file?
    ///     e) Which classes need to change and how if we want to have achievements that exclude unlocking other achievements
    ///     f) What is the concern of the AwardAchievement method of the AwardService?
    /// </summary>
    class Achievement
    {
        public string Name { get; }
        public string ImagePath { get; }
        private readonly IList<string> _prerequisiteAchievementNames;

        public Achievement(string name, string imagePath, string[] prerequisiteAchievements)
        {
            Name = name;
            ImagePath = imagePath;
            _prerequisiteAchievementNames = new List<string>(prerequisiteAchievements);
        }

        public override bool Equals(object other)
        {
            if (!(other is Achievement otherMember)) return false;
            return Name.Equals(otherMember.Name);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool PrerequisitesFulfilled(ISet<Achievement> unlockedAchievements)
        {
            var achievementNames = unlockedAchievements.Select(a => a.Name).ToHashSet();

            foreach (var prerequisite in _prerequisiteAchievementNames)
            {
                if(achievementNames.Contains(prerequisite)) continue;
                return false;
            }

            return true;
        }
    }

    class AchievementRepository
    {
        private readonly string _achievementStorageLocation = "C:/MyGame/Storage/Achievements/";

        public void SaveAchievement(int userId, Achievement newAchievement)
        {
            string newAchievementStorageFormat = newAchievement.Name + ":" + newAchievement.ImagePath + "\n";
            File.AppendAllText(_achievementStorageLocation + userId + ".csv", newAchievementStorageFormat);
        }

        public AchievementCollection LoadAchievementCollection(int userId)
        {
            string[] achievements = File.ReadAllLines(_achievementStorageLocation + userId + ".csv");
            ISet<Achievement> unlockedAchievements = new HashSet<Achievement>();
            foreach (var storedAchievement in achievements)
            {
                string[] achievementElements = storedAchievement.Split(":");
                Achievement a = new Achievement(achievementElements[0], achievementElements[1], null);
                unlockedAchievements.Add(a);
            }

            return new AchievementCollection(userId, unlockedAchievements);
        }

        public Achievement LoadAchievement(int newAchievementId)
        {
            string[] allAchievements = File.ReadAllLines(_achievementStorageLocation + "allAchievements.csv");
            foreach (var achievement in allAchievements)
            {
                string[] achievementElements = achievement.Split(":");
                if (!achievementElements[0].Equals(newAchievementId.ToString())) continue;

                string[] prerequisites = achievementElements.Length == 2 ? null : achievementElements[new Range(2, achievementElements.Length)];
                Achievement newAchievement = new Achievement(achievementElements[0], achievementElements[1], prerequisites);

                return newAchievement;
            }

            return null;
        }
    }

    class AchievementService
    {
        private readonly AchievementRepository _achievementRepository = new AchievementRepository();

        public void AwardAchievement(int userId, int newAchievementId)
        {
            Achievement newAchievement = _achievementRepository.LoadAchievement(newAchievementId);
            if (newAchievement == null) throw new Exception("New achievement does not exist in the registry.");
            var achievementCollection = _achievementRepository.LoadAchievementCollection(userId);
            
            achievementCollection.UnlockAchievement(newAchievement);

            _achievementRepository.SaveAchievement(userId, newAchievement);
        }
    }
}
