using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;

namespace UnityEditor.Chapter8.SocialPlatforms
{
    /// <summary>
    /// UnityEngine.SocialPlatforms 社交平台系统案例
    /// 演示社交平台集成、成就系统、排行榜、用户认证等功能
    /// </summary>
    public class SocialPlatformsExample : MonoBehaviour
    {
        [Header("社交平台设置")]
        [SerializeField] private bool autoAuthenticate = true;
        [SerializeField] private bool showLeaderboardUI = true;
        [SerializeField] private bool showAchievementUI = true;
        
        [Header("用户信息")]
        [SerializeField] private string userName = "";
        [SerializeField] private string userID = "";
        [SerializeField] private bool isAuthenticated = false;
        [SerializeField] private Texture2D userImage;
        
        [Header("成就系统")]
        [SerializeField] private List<AchievementData> achievements = new List<AchievementData>();
        [SerializeField] private int totalAchievements = 0;
        [SerializeField] private int unlockedAchievements = 0;
        
        [Header("排行榜")]
        [SerializeField] private List<LeaderboardData> leaderboards = new List<LeaderboardData>();
        [SerializeField] private string currentLeaderboardID = "leaderboard_001";
        [SerializeField] private long currentScore = 0;
        
        [Header("分数设置")]
        [SerializeField] private long scoreToAdd = 100;
        [SerializeField] private string achievementToUnlock = "achievement_001";
        
        [Header("UI控制")]
        [SerializeField] private bool showControls = true;
        
        private bool isInitialized = false;
        
        [System.Serializable]
        public class AchievementData
        {
            public string id;
            public string title;
            public string description;
            public bool isUnlocked;
            public float progress;
            public float maxProgress;
        }
        
        [System.Serializable]
        public class LeaderboardData
        {
            public string id;
            public string title;
            public long score;
            public int rank;
        }
        
        private void Start()
        {
            InitializeSocialPlatforms();
        }
        
        /// <summary>
        /// 初始化社交平台
        /// </summary>
        private void InitializeSocialPlatforms()
        {
            if (autoAuthenticate)
            {
                AuthenticateUser();
            }
            
            InitializeAchievements();
            InitializeLeaderboards();
            
            isInitialized = true;
            Debug.Log("社交平台系统已初始化");
        }
        
        /// <summary>
        /// 用户认证
        /// </summary>
        public void AuthenticateUser()
        {
            Social.localUser.Authenticate((bool success) =>
            {
                isAuthenticated = success;
                if (success)
                {
                    userName = Social.localUser.userName;
                    userID = Social.localUser.id;
                    userImage = Social.localUser.image;
                    Debug.Log($"用户认证成功: {userName}");
                }
                else
                {
                    Debug.Log("用户认证失败");
                }
            });
        }
        
        /// <summary>
        /// 初始化成就系统
        /// </summary>
        private void InitializeAchievements()
        {
            achievements.Clear();
            
            // 添加示例成就
            AddAchievement("achievement_001", "初次登录", "完成首次登录游戏", 1, 1);
            AddAchievement("achievement_002", "得分高手", "获得1000分", 0, 1000);
            AddAchievement("achievement_003", "连续游戏", "连续游戏10分钟", 0, 600);
            AddAchievement("achievement_004", "完美表现", "获得满分", 0, 10000);
            AddAchievement("achievement_005", "社交达人", "分享游戏10次", 0, 10);
            
            totalAchievements = achievements.Count;
            Debug.Log($"初始化成就系统: {totalAchievements} 个成就");
        }
        
        /// <summary>
        /// 添加成就
        /// </summary>
        public void AddAchievement(string id, string title, string description, float currentProgress, float maxProgress)
        {
            AchievementData achievement = new AchievementData
            {
                id = id,
                title = title,
                description = description,
                isUnlocked = currentProgress >= maxProgress,
                progress = currentProgress,
                maxProgress = maxProgress
            };
            
            achievements.Add(achievement);
            
            if (achievement.isUnlocked)
            {
                unlockedAchievements++;
            }
        }
        
        /// <summary>
        /// 解锁成就
        /// </summary>
        public void UnlockAchievement(string achievementID)
        {
            if (!isAuthenticated) return;
            
            Social.ReportProgress(achievementID, 100.0, (bool success) =>
            {
                if (success)
                {
                    // 更新本地成就数据
                    AchievementData achievement = achievements.Find(a => a.id == achievementID);
                    if (achievement != null && !achievement.isUnlocked)
                    {
                        achievement.isUnlocked = true;
                        achievement.progress = achievement.maxProgress;
                        unlockedAchievements++;
                        Debug.Log($"成就解锁: {achievement.title}");
                    }
                }
                else
                {
                    Debug.Log($"成就解锁失败: {achievementID}");
                }
            });
        }
        
        /// <summary>
        /// 更新成就进度
        /// </summary>
        public void UpdateAchievementProgress(string achievementID, float progress)
        {
            if (!isAuthenticated) return;
            
            AchievementData achievement = achievements.Find(a => a.id == achievementID);
            if (achievement != null)
            {
                achievement.progress = Mathf.Min(progress, achievement.maxProgress);
                float percentage = (achievement.progress / achievement.maxProgress) * 100.0f;
                
                Social.ReportProgress(achievementID, percentage, (bool success) =>
                {
                    if (success)
                    {
                        if (achievement.progress >= achievement.maxProgress && !achievement.isUnlocked)
                        {
                            achievement.isUnlocked = true;
                            unlockedAchievements++;
                            Debug.Log($"成就解锁: {achievement.title}");
                        }
                        else
                        {
                            Debug.Log($"成就进度更新: {achievement.title} - {percentage:F1}%");
                        }
                    }
                });
            }
        }
        
        /// <summary>
        /// 显示成就UI
        /// </summary>
        public void ShowAchievementsUI()
        {
            if (!isAuthenticated) return;
            
            Social.ShowAchievementsUI();
            Debug.Log("显示成就界面");
        }
        
        /// <summary>
        /// 初始化排行榜
        /// </summary>
        private void InitializeLeaderboards()
        {
            leaderboards.Clear();
            
            // 添加示例排行榜
            AddLeaderboard("leaderboard_001", "总分排行榜", 0);
            AddLeaderboard("leaderboard_002", "最高分排行榜", 0);
            AddLeaderboard("leaderboard_003", "游戏时长排行榜", 0);
            
            Debug.Log($"初始化排行榜: {leaderboards.Count} 个排行榜");
        }
        
        /// <summary>
        /// 添加排行榜
        /// </summary>
        public void AddLeaderboard(string id, string title, long score)
        {
            LeaderboardData leaderboard = new LeaderboardData
            {
                id = id,
                title = title,
                score = score,
                rank = 0
            };
            
            leaderboards.Add(leaderboard);
        }
        
        /// <summary>
        /// 提交分数
        /// </summary>
        public void SubmitScore(string leaderboardID, long score)
        {
            if (!isAuthenticated) return;
            
            Social.ReportScore(score, leaderboardID, (bool success) =>
            {
                if (success)
                {
                    // 更新本地排行榜数据
                    LeaderboardData leaderboard = leaderboards.Find(l => l.id == leaderboardID);
                    if (leaderboard != null)
                    {
                        leaderboard.score = score;
                        Debug.Log($"分数提交成功: {leaderboard.title} - {score}");
                    }
                }
                else
                {
                    Debug.Log($"分数提交失败: {leaderboardID}");
                }
            });
        }
        
        /// <summary>
        /// 显示排行榜UI
        /// </summary>
        public void ShowLeaderboardUI()
        {
            if (!isAuthenticated) return;
            
            Social.ShowLeaderboardUI();
            Debug.Log("显示排行榜界面");
        }
        
        /// <summary>
        /// 获取排行榜数据
        /// </summary>
        public void LoadLeaderboardData(string leaderboardID)
        {
            if (!isAuthenticated) return;
            
            ILeaderboard leaderboard = Social.CreateLeaderboard();
            leaderboard.id = leaderboardID;
            leaderboard.LoadScores((bool success) =>
            {
                if (success)
                {
                    Debug.Log($"加载排行榜数据成功: {leaderboardID}");
                    Debug.Log($"排行榜标题: {leaderboard.title}");
                    Debug.Log($"排行榜分数范围: {leaderboard.range.min} - {leaderboard.range.max}");
                    
                    IScore[] scores = leaderboard.scores;
                    for (int i = 0; i < scores.Length; i++)
                    {
                        Debug.Log($"第{i + 1}名: {scores[i].userID} - {scores[i].value}");
                    }
                }
                else
                {
                    Debug.Log($"加载排行榜数据失败: {leaderboardID}");
                }
            });
        }
        
        /// <summary>
        /// 分享游戏
        /// </summary>
        public void ShareGame()
        {
            if (!isAuthenticated) return;
            
            // 这里可以实现游戏分享功能
            Debug.Log("分享游戏功能待实现");
            
            // 更新分享成就
            UpdateAchievementProgress("achievement_005", GetAchievementProgress("achievement_005") + 1);
        }
        
        /// <summary>
        /// 获取成就进度
        /// </summary>
        public float GetAchievementProgress(string achievementID)
        {
            AchievementData achievement = achievements.Find(a => a.id == achievementID);
            return achievement != null ? achievement.progress : 0f;
        }
        
        /// <summary>
        /// 获取排行榜分数
        /// </summary>
        public long GetLeaderboardScore(string leaderboardID)
        {
            LeaderboardData leaderboard = leaderboards.Find(l => l.id == leaderboardID);
            return leaderboard != null ? leaderboard.score : 0;
        }
        
        /// <summary>
        /// 添加分数
        /// </summary>
        public void AddScore(long score)
        {
            currentScore += score;
            SubmitScore(currentLeaderboardID, currentScore);
            Debug.Log($"添加分数: {score}, 当前总分: {currentScore}");
        }
        
        /// <summary>
        /// 解锁指定成就
        /// </summary>
        public void UnlockSpecificAchievement()
        {
            UnlockAchievement(achievementToUnlock);
        }
        
        /// <summary>
        /// 获取用户统计信息
        /// </summary>
        public void GetUserStats()
        {
            if (!isAuthenticated) return;
            
            Debug.Log("=== 用户统计信息 ===");
            Debug.Log($"用户名: {userName}");
            Debug.Log($"用户ID: {userID}");
            Debug.Log($"认证状态: {(isAuthenticated ? "已认证" : "未认证")}");
            Debug.Log($"成就进度: {unlockedAchievements}/{totalAchievements}");
            Debug.Log($"当前分数: {currentScore}");
        }
        
        /// <summary>
        /// 重置所有数据
        /// </summary>
        public void ResetAllData()
        {
            // 重置成就
            foreach (AchievementData achievement in achievements)
            {
                achievement.isUnlocked = false;
                achievement.progress = 0;
            }
            unlockedAchievements = 0;
            
            // 重置分数
            currentScore = 0;
            foreach (LeaderboardData leaderboard in leaderboards)
            {
                leaderboard.score = 0;
                leaderboard.rank = 0;
            }
            
            Debug.Log("所有数据已重置");
        }
        
        private void OnGUI()
        {
            if (!showControls) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 400, 700));
            GUILayout.Label("UnityEngine.SocialPlatforms 社交平台系统案例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            
            // 用户认证
            GUILayout.Label("用户认证", EditorStyles.boldLabel);
            if (GUILayout.Button($"认证状态: {(isAuthenticated ? "已认证" : "未认证")}"))
            {
                AuthenticateUser();
            }
            
            if (isAuthenticated)
            {
                GUILayout.Label($"用户名: {userName}");
                GUILayout.Label($"用户ID: {userID}");
            }
            
            GUILayout.Space(10);
            
            // 成就系统
            GUILayout.Label("成就系统", EditorStyles.boldLabel);
            GUILayout.Label($"成就进度: {unlockedAchievements}/{totalAchievements}");
            
            if (GUILayout.Button("显示成就界面"))
            {
                ShowAchievementsUI();
            }
            
            if (GUILayout.Button("解锁指定成就"))
            {
                UnlockSpecificAchievement();
            }
            
            // 成就列表
            GUILayout.Label("成就列表:", EditorStyles.boldLabel);
            foreach (AchievementData achievement in achievements)
            {
                string status = achievement.isUnlocked ? "✓" : "○";
                float percentage = (achievement.progress / achievement.maxProgress) * 100f;
                GUILayout.Label($"{status} {achievement.title}: {percentage:F1}%");
            }
            
            GUILayout.Space(10);
            
            // 排行榜
            GUILayout.Label("排行榜", EditorStyles.boldLabel);
            GUILayout.Label($"当前分数: {currentScore}");
            
            if (GUILayout.Button("显示排行榜界面"))
            {
                ShowLeaderboardUI();
            }
            
            if (GUILayout.Button("加载排行榜数据"))
            {
                LoadLeaderboardData(currentLeaderboardID);
            }
            
            // 分数操作
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("添加分数")) AddScore(scoreToAdd);
            if (GUILayout.Button("分享游戏")) ShareGame();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // 设置
            GUILayout.Label("设置", EditorStyles.boldLabel);
            
            long newScoreToAdd = (long)GUILayout.HorizontalSlider(scoreToAdd, 10, 1000);
            if (newScoreToAdd != scoreToAdd)
            {
                scoreToAdd = newScoreToAdd;
            }
            GUILayout.Label($"添加分数: {scoreToAdd}");
            
            GUILayout.Space(10);
            
            // 功能按钮
            GUILayout.Label("功能", EditorStyles.boldLabel);
            if (GUILayout.Button("获取用户统计"))
            {
                GetUserStats();
            }
            
            if (GUILayout.Button("重置所有数据"))
            {
                ResetAllData();
            }
            
            GUILayout.Space(10);
            
            // 信息显示
            GUILayout.Label("信息", EditorStyles.boldLabel);
            GUILayout.Label($"初始化状态: {(isInitialized ? "已完成" : "未完成")}");
            GUILayout.Label($"成就完成率: {(totalAchievements > 0 ? (float)unlockedAchievements / totalAchievements * 100f : 0f):F1}%");
            
            if (leaderboards.Count > 0)
            {
                LeaderboardData currentLeaderboard = leaderboards.Find(l => l.id == currentLeaderboardID);
                if (currentLeaderboard != null)
                {
                    GUILayout.Label($"当前排行榜: {currentLeaderboard.title}");
                }
            }
            
            GUILayout.Space(10);
            
            // 操作提示
            GUILayout.Label("操作提示", EditorStyles.boldLabel);
            GUILayout.Label("需要配置相应的社交平台SDK");
            GUILayout.Label("在真机上测试社交功能");
            GUILayout.Label("确保网络连接正常");
            
            GUILayout.EndArea();
        }
    }
} 