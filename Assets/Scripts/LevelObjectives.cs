using UnityEngine;

namespace Rubber
{
    public class LevelObjectives : MonoBehaviour
    {
        internal static bool bot = false, top = false, right = false, left = false, b1 = false, b2 = false, b3 = false;

        internal static void ColorInitializer(int Scene2Load, ref Animator animator)
        {
            switch (Scene2Load)
            {
                
                case 21:
                    if (animator.name == "Block1") 
                    {
                        b1 = true;
                        animator.SetInteger("which", 0); break;
                    }
                    animator.SetInteger("which", 1);
                    break;
                case 69:
                    right = true; bot = true; top = true;
                    animator.SetInteger("which", 0); // Resetlendikçe yeşil olacak
                    break;

                default:
                    animator.SetInteger("which", 1);
                    break;
            }
        }

        public static void MakeAllConditionsFalse()
        {
            bot = false; top = false;
            right = false; left = false;
            b1 = false; b2 = false; b3 = false;
        }

        internal static bool CheckIsLevelObjectivesDone(int levelİndex)
        {
            switch (levelİndex)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    return true;
                case 6:
                    if (right) { return true; } else { return false; }

                case 7:
                    if (right && left) { return true; } else { return false; }

                case 8:
                    if (bot && left) { return true; } else { return false; }

                case 9:
                    if (top) { return true; } else { return false; }

                case 10:
                    if (b1) { return true; } else { return false; }

                case 11:
                    if (bot) { return true; } else { return false; }

                case 12:
                    if (bot && right) { return true; } else { return false; }

                case 13:
                    if (right && top) { return true; } else { return false; }

                case 14:
                    if (b1) { return true; } else { return false; }

                case 15:
                    if (b1 && right && top) { return true; } else { return false; }

                case 16:
                    if (b1 && top) { return true; } else { return false; }

                case 17:
                    if (b1 && top) { return true; } else { return false; }

                case 18:
                    if (left && right) { return true; } else { return false; }

                case 19:
                    if (bot && top && b1) { return true; } else { return false; }

                case 20:
                    if (bot && top && right && b1) { return true; } else { return false; }

                case 21:
                    if (b1 && right && left && top) { return true; } else { return false; }

                case 22:
                    if (bot && b1) { return true; } else { return false; }

                case 23:
                    if (bot && right) { return true; } else { return false; }

                case 24:
                    if (b1 && b2) { return true; } else { return false; }

                case 25:
                    if (b1 && b2 && b3) { return true; } else { return false; }

                case 26:
                    if (top) { return true; } else { return false; }

                case 27:
                    if (left && right) { return true; } else { return false; }

                case 28:
                    if (b1 && b2 && right) { return true; } else { return false; }

                case 29:
                    if (b1 && left && right) { return true; } else { return false; }

                case 30:
                    if (top && bot) { return true; } else { return false; }

                case 31:
                    if (b1 && top && right) { return true; } else { return false; }

                case 32:
                    if (b1 && b2 && b3) { return true; } else { return false; }

                case 33:
                    if (right && bot && top) { return true; } else { return false; }

                case 34:
                    if (b1 && right) { return true; } else { return false; }

                case 35:
                    if (bot && top && left) { return true; } else { return false; }

                case 36:
                    if (b1 && bot && left) { return true; } else { return false; }

                case 37:
                    if (b1 && b2 && b3) { return true; } else { return false; }

                case 38:
                    if (b1 && b2) { return true; } else { return false; }

                case 39:
                    if (b1 && bot) { return true; } else { return false; }

                case 40:
                    if (b1 && b2 && top) { return true; } else { return false; }

                case 41:
                    if (b1 && b2 && left) { return true; } else { return false; }

                case 42:
                    if (bot && top && left) { return true; } else { return false; }

                case 43:
                    if (b1 && b2 && bot && right) { return true; } else { return false; }

                case 44:
                    if (b1 && top && left) { return true; } else { return false; }

                case 45:
                    if (b1 && b2 && top) { return true; } else { return false; }

                case 46:
                    if (b1 && b2) { return true; } else { return false; }

                case 47:
                    if (b1 && b2 && b3 && top) { return true; } else { return false; }

                case 48:
                    if (b1 && b2) { return true; } else { return false; }

                case 49:
                    if (left && right && top) { return true; } else { return false; }

                case 50:
                    if (bot && top) { return true; } else { return false; }

                case 51:
                    if (right && left) { return true; } else { return false; }

                case 52:
                    if (right && top) { return true; } else { return false; }

                case 53:
                    if (left && right && top && bot) { return true; } else { return false; }

                case 54:
                    if (left && right && top && bot) { return true; } else { return false; }

                case 55:
                    if (left && right && top && bot) { return true; } else { return false; }

                case 56:
                    if (top && left && right) { return true; } else { return false; }

                case 57:
                    if (b1 && b2) { return true; } else { return false; }

                case 58:
                    if (b1 && top && right) { return true; } else { return false; }

                case 59:
                    if (bot && top) { return true; } else { return false; }

                case 60:
                    if (b1 && right && bot && left) { return true; } else { return false; }

                case 61:
                    if (right && top && left && b1) { return true; } else { return false; }

                case 62:
                    if (bot) { return true; } else { return false; }

                case 63:
                    if (bot && left && right) { return true; } else { return false; }

                case 64:
                    if (bot && left && right) { return true; } else { return false; }

                case 65:
                    if (right && top) { return true; } else { return false; }

                case 66:
                    if (top && right) { return true; } else { return false; }

                case 67:
                    if (b1 && right && left) { return true; } else { return false; }

                case 68:
                    if (bot && right && b1) { return true; } else { return false; }

                case 69:
                    if (bot && right && top) { return true; } else { return false; }

                case 70:
                    return true;
                case 71:
                    if (bot && right && left) { return true; } else { return false; }

                case 72:
                    if (left && right && bot) { return true; } else { return false; }

                case 73:
                    if (bot && top && left && right) { return true; } else { return false; }

                case 74:
                    if (b1 && b2) { return true; } else { return false; }

                case 75:
                    if (b1 && right && bot) { return true; } else { return false; }
            }
            return false;
        }
    }
}
