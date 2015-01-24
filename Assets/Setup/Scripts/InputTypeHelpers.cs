public class InputTypeHelpers
{
    public static string GetIconByType(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Pad1Left: return "input_pad1left";
            case InputType.Pad1Right: return "input_pad1right";
            case InputType.Pad2Left: return "input_pad2left";
            case InputType.Pad2Right: return "input_pad2right";
            case InputType.Wasd: return "input_wasd";
            case InputType.Ijkl: return "input_ijkl";
            case InputType.Arrows: return "input_arrows";
            case InputType.Numpad5123: return "input_5123";
        }

        return null;
    }
}
