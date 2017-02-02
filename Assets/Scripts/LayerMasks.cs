using UnityEngine;
using System.Collections;

public static class LayerMasks {
    private static int player = 10;
    private static int enemy = 9;
    private static int terrain = 8;
    public static LayerMask terrainOnly = 1 << terrain;
    public static LayerMask playerOnly = 1 << player;
    public static LayerMask terrainAndPlayer = (1 << terrain) | (1 << player);
    public static LayerMask ignorePlayer = ~(1 << player);
    public static LayerMask ignoreCharacters = ~((1 << player) | (1 << enemy));
    public static LayerMask ignoreEnemies = ~(1 << enemy);

}
