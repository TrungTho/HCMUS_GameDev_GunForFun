public class Weapon {
    public int typeOfGun;
    public string gunName;
    public float power;
    public float howFar;
    public float rateOfFire;
    public int numberOfAmmo;
    public float reloadTime;
}

public static class ListOfWeapon {
    public static Weapon selectedWeapon = new Weapon() {
        typeOfGun = 1,
        gunName = "Scope Pistol",
        power = 700f,
        howFar = 40f,
        rateOfFire = 0.2f,
        numberOfAmmo = 7,
        reloadTime = 1f
    };

    public static Weapon getGun(int typeOfGun, int numberOfGun) {
        switch (typeOfGun) {
            case 1: return ListOfWeapon.handguns[numberOfGun - 1];
            case 2: return ListOfWeapon.rifles[numberOfGun - 1];
            case 3: return ListOfWeapon.shotguns[numberOfGun - 1];
            case 4: return ListOfWeapon.snipers[numberOfGun - 1];
            default:
                return ListOfWeapon.handguns[0];
        }
    }

    public static Weapon[] handguns = new Weapon[] {
        new Weapon(){typeOfGun=1, gunName = "Acient", power= 800f, howFar= 40f, rateOfFire= 0.3f, numberOfAmmo=7, reloadTime=1.5f},
        new Weapon(){typeOfGun=1, gunName = "Glock26", power= 500f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=7, reloadTime=1f},
        new Weapon(){typeOfGun=1, gunName = "Barreta21A", power= 1000f, howFar= 40f, rateOfFire= 0.3f, numberOfAmmo=10, reloadTime=1f},
        new Weapon(){typeOfGun=1, gunName = "ClassicalHandgun", power= 600f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=8, reloadTime=1.5f},
        new Weapon(){typeOfGun=1, gunName = "colt", power= 500f, howFar= 40f, rateOfFire= 0.3f, numberOfAmmo=7, reloadTime=1f},
        new Weapon(){typeOfGun=1, gunName = "Colt45", power= 600f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=8, reloadTime=1.5f},
        new Weapon(){typeOfGun=1, gunName = "GoldenColt", power= 1200f, howFar= 40f, rateOfFire= 0.3f, numberOfAmmo=10, reloadTime=1f},
        new Weapon(){typeOfGun=1, gunName = "Mini Pistol", power= 500f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=7, reloadTime=1.5f},
        new Weapon(){typeOfGun=1, gunName = "heavycolt", power= 800f, howFar= 40f, rateOfFire= 0.15f, numberOfAmmo=10, reloadTime=2f},
        new Weapon(){typeOfGun=1, gunName = "Mini Anaconda", power= 500f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=10, reloadTime=1.5f},
        new Weapon(){typeOfGun=1, gunName = "Luger", power= 600f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=8, reloadTime=1f},
        new Weapon(){typeOfGun=1, gunName = "Scope Pistol", power= 700f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=7, reloadTime=1f},
        new Weapon(){typeOfGun=1, gunName = "MiniColt", power= 500f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=7, reloadTime=1.5f},
        new Weapon(){typeOfGun=1, gunName = "Nagan", power= 500f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=10, reloadTime=1f},
        new Weapon(){typeOfGun=1, gunName = "Coltana", power= 500f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=7, reloadTime=1f},
     };

    public static Weapon[] rifles = new Weapon[] {
        new Weapon(){typeOfGun=2, gunName = "AK47", power= 800f, howFar= 40f, rateOfFire= 0.1f, numberOfAmmo=30, reloadTime=2f},
        new Weapon(){typeOfGun=2, gunName = "TaticalSMGs", power= 500f, howFar= 40f, rateOfFire= 0.15f, numberOfAmmo=25, reloadTime=2.5f},
        new Weapon(){typeOfGun=2, gunName = "AEK-999", power= 600f, howFar= 40f, rateOfFire= 0.05f, numberOfAmmo=50, reloadTime=3f},
        new Weapon(){typeOfGun=2, gunName = "M4A1", power= 700f, howFar= 40f, rateOfFire= 0.05f, numberOfAmmo=30, reloadTime=2f},
        new Weapon(){typeOfGun=2, gunName = "MG42", power= 600f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=10, reloadTime=1.5f},
        new Weapon(){typeOfGun=2, gunName = "MP40", power= 800f, howFar= 40f, rateOfFire= 0.1f, numberOfAmmo=40, reloadTime=2f},
        new Weapon(){typeOfGun=2, gunName = "PPSH", power= 800f, howFar= 40f, rateOfFire= 0.15f, numberOfAmmo=50, reloadTime=2.5f},
        new Weapon(){typeOfGun=2, gunName = "SilentRifle", power= 450f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=10, reloadTime=1.5f},
        new Weapon(){typeOfGun=2, gunName = "STG44", power= 500f, howFar= 40f, rateOfFire= 0.1f, numberOfAmmo=30, reloadTime=2f},
        new Weapon(){typeOfGun=2, gunName = "Tompson", power= 400f, howFar= 40f, rateOfFire= 0.05f, numberOfAmmo=40, reloadTime=2.5f},
        new Weapon(){typeOfGun=2, gunName = "FancySMGs", power= 600f, howFar= 40f, rateOfFire= 0.05f, numberOfAmmo=30, reloadTime=1.5f},
        new Weapon(){typeOfGun=2, gunName = "AntiqueSMGs", power= 800f, howFar= 40f, rateOfFire= 0.15f, numberOfAmmo=30, reloadTime=2f},
        new Weapon(){typeOfGun=2, gunName = "Uzi", power= 400f, howFar= 40f, rateOfFire= 0.01f, numberOfAmmo=30, reloadTime=1.5f},
     };

    public static Weapon[] shotguns = new Weapon[] {
        new Weapon(){typeOfGun=3, gunName = "AntiCraft", power= 1500f, howFar= 5f, rateOfFire= 0.4f, numberOfAmmo=20, reloadTime=3f},
        new Weapon(){typeOfGun=3, gunName = "KSG", power= 1800f, howFar= 5f, rateOfFire= 0.5f, numberOfAmmo=15, reloadTime=2f},
        new Weapon(){typeOfGun=3, gunName = "MAG7", power= 1500f, howFar= 5f, rateOfFire= 0.4f, numberOfAmmo=12, reloadTime=2f},
        new Weapon(){typeOfGun=3, gunName = "RGA-86", power= 1700f, howFar= 5f, rateOfFire= 0.4f, numberOfAmmo=15, reloadTime=2f},
        new Weapon(){typeOfGun=3, gunName = "Saiga-12", power= 1500f, howFar= 5f, rateOfFire= 0.5f, numberOfAmmo=13, reloadTime=2.5f},
        new Weapon(){typeOfGun=3, gunName = "TOZ-106", power= 1500f, howFar= 5f, rateOfFire= 0.6f, numberOfAmmo=10, reloadTime=2f},
        new Weapon(){typeOfGun=3, gunName = "UTS15", power= 1800f, howFar= 5f, rateOfFire= 0.4f, numberOfAmmo=12, reloadTime=2.5f},
        new Weapon(){typeOfGun=3, gunName = "Winchester37", power= 1800f, howFar= 5f, rateOfFire= 0.4f, numberOfAmmo=10, reloadTime=3f},
        new Weapon(){typeOfGun=3, gunName = "BrowningAuto5", power= 1700f, howFar= 5f, rateOfFire= 0.35f, numberOfAmmo=12, reloadTime=2.5f},
        new Weapon(){typeOfGun=3, gunName = "ClassicalShotgun", power= 1500f, howFar= 5f, rateOfFire= 0.4f, numberOfAmmo=15, reloadTime=2.5f},
        new Weapon(){typeOfGun=3, gunName = "GangsterShotgun", power= 1600f, howFar= 5f, rateOfFire= 0.5f, numberOfAmmo=12, reloadTime=3f},
     };

    public static Weapon[] snipers = new Weapon[] {
        new Weapon(){typeOfGun=4, gunName = "AS50", power= 1600f, howFar= 40f, rateOfFire= 0.5f, numberOfAmmo=10, reloadTime=2f},
        new Weapon(){typeOfGun=4, gunName = "RapidSniper", power= 1800f, howFar= 40f, rateOfFire= 0.5f, numberOfAmmo=10, reloadTime=3f},
        new Weapon(){typeOfGun=4, gunName = "Blaser-R93", power= 1500f, howFar= 40f, rateOfFire= 0.4f, numberOfAmmo=12, reloadTime=3f},
        new Weapon(){typeOfGun=4, gunName = "G43", power= 1300f, howFar= 40f, rateOfFire= 0.4f, numberOfAmmo=12, reloadTime=2f},
        new Weapon(){typeOfGun=4, gunName = "HuntingSniper", power= 1600f, howFar= 40f, rateOfFire= 0.5f, numberOfAmmo=8, reloadTime=2.5f},
        new Weapon(){typeOfGun=4, gunName = "L42A1", power= 1500f, howFar= 40f, rateOfFire= 0.4f, numberOfAmmo=12, reloadTime=2f},
        new Weapon(){typeOfGun=4, gunName = "SpaceSniper", power= 1600f, howFar= 40f, rateOfFire= 0.6f, numberOfAmmo=10, reloadTime=2f},
        new Weapon(){typeOfGun=4, gunName = "TaticalSniper", power= 1500f, howFar= 40f, rateOfFire= 0.6f, numberOfAmmo=10, reloadTime=2.5f},
     };

    public static Weapon[] bazooka = new Weapon[] {
         new Weapon(){typeOfGun=1, gunName = "Bazooka", power= 2500f, howFar= 40f, rateOfFire= 0.5f, numberOfAmmo=15, reloadTime=4f},
         new Weapon(){typeOfGun=1, gunName = "minibomb", power= 2000f, howFar= 40f, rateOfFire= 0.2f, numberOfAmmo=20, reloadTime=3f},
      };

}
