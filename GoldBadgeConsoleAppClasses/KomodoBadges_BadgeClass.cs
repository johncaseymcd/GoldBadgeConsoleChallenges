﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeConsoleAppClasses
{
    public class Badge
    {
        public int BadgeID { get; set; }
        public List<string> BadgeAccess { get; set; }
        public bool IsActive { get; set; }

        public Badge() { }

        public Badge(int badgeID, List<string> badgeAccess, bool isActive)
        {
            BadgeID = badgeID;
            BadgeAccess = badgeAccess;
            IsActive = true;
        }
    }

    public class BadgeCRUD
    {
        private Dictionary<int, Badge> _allBadges = new Dictionary<int, Badge>();

        // Helper method to find badge by ID
        private Badge FindBadgeByID(int badgeID)
        {
            foreach(var badge in _allBadges)
            {
                if (badge.Value.BadgeID == badgeID) 
                {
                    return badge.Value;
                }
            }

            return null;
        }

        // Activate a new badge
        public void ActivateNewBadge(Badge newBadge)
        {
            int badgeCount = _allBadges.Count + 10000;
            _allBadges.Add(badgeCount, newBadge);
        }

        // Get all badges
        public Dictionary<int, Badge> ViewAllBadges()
        {
            return _allBadges;
        }

        // Edit badge access (badge numbers should not be editable)
        public bool EditBadgeAccess(int oldBadgeID, Badge newBadgeAccess)
        {
            var editBadge = FindBadgeByID(oldBadgeID);

            if(editBadge != null)
            {
                editBadge.BadgeAccess = newBadgeAccess.BadgeAccess;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Deactivate a badge
        public bool DeactivateBadge(int badgeID)
        {
            var deactivateBadge = FindBadgeByID(badgeID);
            
            if (deactivateBadge != null)
            {
                deactivateBadge.IsActive = false;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
