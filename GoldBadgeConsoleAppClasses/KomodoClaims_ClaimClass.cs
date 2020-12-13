using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeConsoleAppClasses
{
    public enum ClaimType
    {
        Vehicle = 1,
        Home,
        Theft
    }

    public class Claim
    {
        public int ClaimID { get; set; }
        public ClaimType ClaimType{ get; set; }
        public string ClaimDescription { get; set; }
        public decimal ClaimAmount { get; set; }
        public DateTime LossDate { get; set; }
        public DateTime ClaimDate { get; set; }
        public bool IsValid { get; set; }

        public Claim() { }
        public Claim(int claimID, ClaimType claimType, string claimDescription, decimal claimAmount, DateTime lossDate, DateTime claimDate, bool isValid)
        {
            ClaimID = claimID;
            ClaimType = claimType;
            ClaimDescription = claimDescription;
            ClaimAmount = claimAmount;
            LossDate = lossDate;
            ClaimDate = claimDate;
            IsValid = isValid;
        }
    }

    public class ClaimCRUD
    {
        private Queue<Claim> _allClaims = new Queue<Claim>();

        // Helper method to find claims by Claim ID
        public Claim FindClaimByID(int claimID)
        {
            foreach (var claim in _allClaims)
            {
                if (claim.ClaimID == claimID)
                {
                    return claim;
                }
            }

            return null;
        }
        // Create a new claim and add it to the queue
        public void CreateClaim(Claim newClaim)
        {
            _allClaims.Enqueue(newClaim);
        }

        // Return all claims in the queue 
        public Queue<Claim> GetAllClaims()
        {
            return _allClaims;
        }

        // Delete a claim from the queue
        public bool DeleteClaim(int claimID)
        {
            foreach(var claim in _allClaims)
            {
                if (claim.ClaimID == claimID)
                {
                    var deletedClaim = _allClaims.Dequeue();
                    if (deletedClaim != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}
