using UnityEngine;

[System.Serializable]
public class Park_SongData
{
    // 노래 제목
    public string title;

    // 노래 아티스트
    public string artist;

    // 노래 난이도
    public int difficulty;

    // 노래 최고 점수
    public float bestScore;

    // 노래 이미지
    public Sprite backgroundImg;

    public Park_SongData(string title_, string artist_, int difficulty_, float bestScore_, Sprite backgroundImg_)
    {
        this.title = title_;
        this.artist = artist_;
        this.difficulty = difficulty_;
        this.bestScore = bestScore_;
        this.backgroundImg = backgroundImg_;
    }
}
