using UnityEngine;

//~Struct for Letter construction; ties sprite and letter value together
public struct Letter {
    public Sprite letter_spr;
    public char letter_char;
    public int letter_value;

    public Letter(Sprite letter_spr, char letter_char, int letter_value) {
        this.letter_spr = letter_spr;
        this.letter_char = letter_char;
        this.letter_value = letter_value;
    }
}