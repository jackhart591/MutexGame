using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum VariableColor { Blue, Red, Green, Yellow };

public static class VariableColorManager {

    private static Dictionary<VariableColor, Color> colorTranslator;

    static VariableColorManager() {
        colorTranslator = new Dictionary<VariableColor, Color> {
            { VariableColor.Blue, Color.blue },
            { VariableColor.Red, Color.red },
            { VariableColor.Green, Color.green },
            { VariableColor.Yellow, Color.yellow }
        };
    }

    public static Color? GetColorFromEnum(VariableColor col) {
        if (colorTranslator.ContainsKey(col)) { return colorTranslator[col]; }

        return null;
    }

    public static VariableColor? GetEnumFromColor(Color col) {
        if (colorTranslator.ContainsValue(col)) {
            return colorTranslator.FirstOrDefault(x => x.Value == col).Key;
        }

        return null;
    }
}
