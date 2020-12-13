using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldBadgeConsoleAppClasses;

namespace GoldBadgeConsoleChallengeConsole_Badges
{
    class KomodoBadge_ProgramUI
    {
        private List<Room> _allRooms = new List<Room>();

        private List<Room> _floor1 = new List<Room>();
       
        private List<Room> _floor2 = new List<Room>();

        private List<Room> _floor3 = new List<Room>();

        private List<Room> _floor4 = new List<Room>();

        private List<Room> _floor5 = new List<Room>();

        private Dictionary<int, Badge> _allBadges = new Dictionary<int, Badge>();

        BadgeCRUD badgeManipulator = new BadgeCRUD();

        public void Run()
        {
            Console.SetWindowSize(200, 40);
            SeedBadgeList();
            MainMenu();
        }

        private void MainMenu()
        {
        MainMenu:
            _allBadges = badgeManipulator.GetAllBadges();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome! What would you like to do?\n\n" +
                "1. Activate a new badge\n" +
                "2. View all badges\n" +
                "3. Edit room access for a badge\n" +
                "4. Deactivate a badge\n" +
                "5. Exit");
            string inputChoice = Console.ReadLine();
            bool parseChoice = int.TryParse(inputChoice, out int whatToDo);
            if (parseChoice)
            {
                switch (whatToDo)
                {
                    case 1:
                        ActivateBadge();
                        goto MainMenu;
                    case 2:
                        ViewBadges();
                        Console.ReadKey();
                        goto MainMenu;
                    case 3:
                        EditAccess();
                        goto MainMenu;
                    case 4:
                        DeactivateBadge();
                        goto MainMenu;
                    case 5:
                        Console.WriteLine("Press any key to exit or press H to return to the home screen.");
                        if (Console.ReadKey().Key == ConsoleKey.H)
                        {
                            goto MainMenu;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        break;
                    default:
                        PressAnyKey();
                        goto MainMenu;
                }
            }
        }

        // Helper method to handle errors
        private void PressAnyKey()
        {
            Console.WriteLine("Invalid input. Please try again.");
            Console.ReadKey();
        }

        // Helper method to display all rooms
        private void ViewAllRooms()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Floor 1 - Main lobby and client meeting rooms:");
            foreach(var room in _floor1)
            {
                Console.WriteLine($"Room {room.RoomNumber}");
            }
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Floor 2 - Mail room and data entry department");
            foreach(var room in _floor2)
            {
                Console.WriteLine($"Room {room.RoomNumber}");
            }
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Floor 3 - Call center");
            foreach(var room in _floor3)
            {
                Console.WriteLine($"Room {room.RoomNumber}");
            }
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Floor 4 - Claims department and IT department");
            foreach(var room in _floor4)
            {
                Console.WriteLine($"Room {room.RoomNumber}");
            }
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Floor 5 - Executive offices and HR department");
            foreach(var room in _floor5)
            {
                Console.WriteLine($"Room {room.RoomNumber}");
            }
            Console.WriteLine("");
        }

        // Helper method to display all rooms that a badge has access to
        private void DisplayRoomAccess(int badgeID)
        {
            var badge = badgeManipulator.FindBadgeByID(badgeID);
            if (badge.IsActive)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Badge #{badge.BadgeID} is active and has access to the following rooms:");
                foreach (var room in badge.BadgeAccess)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    switch (room.Floor)
                    {
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        case 5:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                    }
                    if (room == badge.BadgeAccess.Last<Room>())
                    {
                        Console.Write($"{room.RoomNumber}");
                    }
                    else
                    {
                        Console.Write($"{room.RoomNumber} | ");
                    }
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine($"Badge #{badge.BadgeID} has been deactivated.");
            }
            
        }

        private void ActivateBadge()
        {
            ViewAllRooms();
            Console.ForegroundColor = ConsoleColor.White;
            var newBadgeAccess = new List<Room>();
            string inputRoomNumber;
            do
            {
            EnterRoomAccess:
                Console.WriteLine("Enter a room number to add access for this badge. To add an entire floor at once, just press the first number (i.e. press 2 to grant access to all of Floor 2). Press F when finished:");
                inputRoomNumber = Console.ReadLine().ToLower();
                bool parseRoomNumber = int.TryParse(inputRoomNumber, out int whichRoom);
                if (parseRoomNumber && inputRoomNumber.Length == 1)
                {
                    switch (whichRoom)
                    {
                        case 1:
                            newBadgeAccess.AddRange(_floor1);
                            goto EnterRoomAccess;
                        case 2:
                            newBadgeAccess.AddRange(_floor2);
                            goto EnterRoomAccess;
                        case 3:
                            newBadgeAccess.AddRange(_floor3);
                            goto EnterRoomAccess;
                        case 4:
                            newBadgeAccess.AddRange(_floor4);
                            goto EnterRoomAccess;
                        case 5:
                            newBadgeAccess.AddRange(_floor5);
                            goto EnterRoomAccess;
                        default:
                            PressAnyKey();
                            goto EnterRoomAccess;
                    }
                }
                else if (parseRoomNumber && inputRoomNumber.Length == 3)
                {
                    newBadgeAccess.Add(new Room(int.Parse(whichRoom.ToString().Substring(0, 1)), whichRoom));
                }
                else if (!parseRoomNumber && inputRoomNumber != "f")
                {
                    PressAnyKey();
                    goto EnterRoomAccess;
                }
            } while (inputRoomNumber != "f");

            var newBadge = new Badge(newBadgeAccess);
            badgeManipulator.ActivateNewBadge(newBadge);
        }

        private void ViewBadges()
        {
            Console.Clear();
            foreach(var badge in _allBadges)
            {
                DisplayRoomAccess(badge.Key);
            }
        }

        private void EditAccess()
        {
            Console.Clear();
        EditRoomAccess:
            ViewBadges();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter the badge ID to edit:");
            string inputBadgeID = Console.ReadLine().ToLower();
            bool parseBadgeID = int.TryParse(inputBadgeID, out int editBadgeID);
            if (parseBadgeID)
            {
                Console.Clear();
                var badgeToEdit = badgeManipulator.FindBadgeByID(editBadgeID);
                DisplayRoomAccess(editBadgeID);
                string inputOption;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Enter A + Room Number (i.e. A240) to add access, enter R + Room Number (i.e. R460) to revoke access, or press F to finish:");
                    inputOption = Console.ReadLine().ToLower();
                    if (inputOption.StartsWith("a") & inputOption.Length == 4)
                    {
                        foreach (var room in _allRooms)
                        {
                            if (int.Parse(inputOption.Remove(0, 1)) == room.RoomNumber && !badgeToEdit.BadgeAccess.Contains(room))
                            {
                                badgeToEdit.BadgeAccess.Add(room);
                            }
                        }
                    }
                    else if (inputOption.StartsWith("r") && inputOption.Length == 4)
                    {
                        foreach (var room in _allRooms)
                        {
                            if (int.Parse(inputOption.Remove(0, 1)) == room.RoomNumber && badgeToEdit.BadgeAccess.Contains(room))
                            {
                                badgeToEdit.BadgeAccess.Remove(room);
                            }
                        }
                    }
                } while (inputOption != "f");
            }
            else
            {
                PressAnyKey();
                goto EditRoomAccess;
            }
        }

        private void DeactivateBadge()
        {

        }

        private void SeedBadgeList()
        {
            var room100 = new Room(1, 100);
            _floor1.Add(room100);
            var room120 = new Room(1, 120);
            _floor1.Add(room120);
            var room140 = new Room(1, 140);
            _floor1.Add(room140);
            var room160 = new Room(1, 160);
            _floor1.Add(room160);
            var room180 = new Room(1, 180);
            _floor1.Add(room180);

            var room200 = new Room(2, 200);
            _floor2.Add(room200);
            var room220 = new Room(2, 220);
            _floor2.Add(room220);
            var room240 = new Room(2, 240);
            _floor2.Add(room240);
            var room260 = new Room(2, 260);
            _floor2.Add(room260);
            var room280 = new Room(2, 280);
            _floor2.Add(room280);

            var room300 = new Room(3, 300);
            _floor3.Add(room300);
            var room320 = new Room(3, 320);
            _floor3.Add(room320);
            var room340 = new Room(3, 340);
            _floor3.Add(room340);
            var room360 = new Room(3, 360);
            _floor3.Add(room360);
            var room380 = new Room(3, 380);
            _floor3.Add(room380);

            var room400 = new Room(4, 400);
            _floor4.Add(room400);
            var room420 = new Room(4, 420);
            _floor4.Add(room420);
            var room440 = new Room(4, 440);
            _floor4.Add(room440);
            var room460 = new Room(4, 460);
            _floor4.Add(room460);
            var room480 = new Room(4, 480);
            _floor4.Add(room480);

            var room500 = new Room(5, 500);
            _floor5.Add(room500);
            var room520 = new Room(5, 520);
            _floor5.Add(room520);
            var room540 = new Room(5, 540);
            _floor5.Add(room540);
            var room560 = new Room(5, 560);
            _floor5.Add(room560);
            var room580 = new Room(5, 580);
            _floor5.Add(room580);

            _allRooms.AddRange(_floor1);
            _allRooms.AddRange(_floor2);
            _allRooms.AddRange(_floor3);
            _allRooms.AddRange(_floor4);
            _allRooms.AddRange(_floor5);

            var dataEntryAccess = new List<Room>();
            dataEntryAccess.AddRange(_floor1);
            dataEntryAccess.AddRange(_floor2);
            var dataEntryBadge = new Badge(dataEntryAccess);
            badgeManipulator.ActivateNewBadge(dataEntryBadge);

            var callCenterAccess = new List<Room>();
            callCenterAccess.AddRange(_floor1);
            callCenterAccess.AddRange(_floor2);
            callCenterAccess.AddRange(_floor3);
            var callCenterBadge = new Badge(callCenterAccess);
            badgeManipulator.ActivateNewBadge(callCenterBadge);

            var claimsAccess = new List<Room>();
            claimsAccess.AddRange(_floor1);
            claimsAccess.AddRange(_floor2);
            claimsAccess.AddRange(_floor3);
            claimsAccess.Add(room400);
            claimsAccess.Add(room420);
            var claimsBadge = new Badge(claimsAccess);
            badgeManipulator.ActivateNewBadge(claimsBadge);

            var itAccess = new List<Room>();
            itAccess.AddRange(_floor1);
            itAccess.AddRange(_floor2);
            itAccess.AddRange(_floor3);
            itAccess.AddRange(_floor4);
            var itBadge = new Badge(itAccess);
            badgeManipulator.ActivateNewBadge(itBadge);

            var executiveAccess = new List<Room>();
            executiveAccess.AddRange(_allRooms);
            var executiveBadge = new Badge(executiveAccess);
            badgeManipulator.ActivateNewBadge(executiveBadge);

            _allBadges = badgeManipulator.GetAllBadges();
        }
    }
}
