using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Database : MonoBehaviour
{
    const string filej = "/Database.json";
    const string path = "Assets/HaDuyBach_Script";
    public List<MucTieuData> listOfKPI = new List<MucTieuData>();
    public ThongTinCaNhanData thongTinCaNhan; 
    public void saveData(List<MucTieuData> list)
    {
        foreach (var _data in listOfKPI)
        {
            _data.tab = null;
            foreach (var tc in _data.listTieuChi)
            {
                tc.body = null;

                foreach (var cv in tc.listCongViec)
                {
                    cv.body = null;
                }
            }
        }

        JsonSerializer serializer = new JsonSerializer();
        serializer.Converters.Add(new JavaScriptDateTimeConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;

        using (StreamWriter sw = new StreamWriter(Application.dataPath + filej))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, list); //loaned is the name of the list.
        }
    }    
    public List<MucTieuData> loadData()
    {
        List<MucTieuData> list;
        
        JsonSerializer serializer = new JsonSerializer();
        serializer.Converters.Add(new JavaScriptDateTimeConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;

        using (StreamReader file = File.OpenText(Application.dataPath + filej))
        {
            list = JsonConvert.DeserializeObject<List<MucTieuData>>(File.ReadAllText(Application.dataPath + filej));
            serializer = new JsonSerializer();
            list = (List<MucTieuData>)serializer.Deserialize(file, typeof(List<MucTieuData>));
        }

        return list;
    }    
    public void testData()
    {
        //listOfKPI = new List<MucTieuData>
        //{
        //    new MucTieuData("Làm Đẹp","Chăm chút ngoại hình",30,null,1,new List<TieuChiData>
        //    {
        //        new TieuChiData("Dưỡng da","Chăm sóc da từ bên trong",15,6,"buổi",null, new List<CongViecData>{
        //            new CongViecData("Uống nước","Uống nhiều nước tốt sức khỏe",2,"lít nước","12/5/2024","14/5/2024",null),
        //            new CongViecData("Uống trà thảo mộc","Uống trà đẹp da",1,"lít trà","12/5/2024","12/4/2024",null),
        //            new CongViecData("Bôi kem dưỡng da","Giúp da mịn hơn",5,"lần","12/5/2024","14/5/2024",null)
        //        }),
        //        new TieuChiData("Dưỡng tóc","Tóc chắc khẻo",75,6,"buổi",null, new List<CongViecData>
        //        {
        //            new CongViecData("Uống thực phẩm chức năng","Cho tóc đen hơn",3,"lần","12/5/2024","12/5/2024",null),
        //            new CongViecData("Dùng dầu dưỡng tóc","Cho tóc mượt hơn",3,"lần","12/5/2024","12/5/2024",null)
        //        })
        //    }),
        //    new MucTieuData("Tập lái xe tăng","Lái vù vù",30,null,1,new List<TieuChiData>{}),
        //    new MucTieuData("Tập tầm vông","tay hông hông có, tập làm chó",30,null,1,new List<TieuChiData>{}),
        //};

        listOfKPI = new List<MucTieuData>
        {
            new MucTieuData("Giảng dạy","",40,null,1,new List<TieuChiData>
            {
                new TieuChiData("Giảng dạy trên trường","",60,10,"giờ",null, new List<CongViecData>{
                    new CongViecData("Dạy lớp 14035","",3,"giờ","31/5/2024","30/6/2024",null),
                    new CongViecData("Dạy lớp 14036","",3,"giờ","30/5/2024","30/6/2024",null),
                }),
                new TieuChiData("Quay video bài học","",40,10,"giờ",null, new List<CongViecData>{
                    new CongViecData("UIUX","",5,"giờ","27/5/2024","30/6/2024",null),
                    new CongViecData("KTPM","",2,"giờ","30/5/2024","4/6/2024",null),
                }),
            }),
            new MucTieuData("Nghiên cứu","",30,null,1,new List<TieuChiData>
            {
                new TieuChiData("Số bài báo","",80,10,"bài",null, new List<CongViecData>{
                    new CongViecData("Nghiên cứu trải nghiệm người dùng","",10,"bài","1/5/2024","31/5/2024",null),
                }),
                new TieuChiData("Số sinh viên mới vào lab","",20,5,"người",null, new List<CongViecData>{
                    new CongViecData("Định hướng sinh viên","",2,"người","30/5/2024","16/6/2024",null),
                }),
            }),
            new MucTieuData("Phục vụ","",30,null,1,new List<TieuChiData>
            {
                new TieuChiData("Thuyết Trình Hội Thảo","",100,5,"buổi",null, new List<CongViecData>{
                    new CongViecData("Hội thảo UIUX","",2,"buổi","29/5/2024","29/6/2024",null),
                    new CongViecData("Hội thảo OOP","",2,"buổi","29/5/2024","15/6/2024",null),
                }),
            }),
        };


        thongTinCaNhan = new ThongTinCaNhanData("Hà Duy Bách", "hhaduybach@gmail.com", "09999999", "0000", "Sinh Viên");
    }
    public void Awake()
    {
        if (FindObjectsOfType<Database>().Length <= 1)
        {
            DontDestroyOnLoad(this);
            testData();
        }
        else
            Destroy(gameObject);

        //listOfKPI = loadData();
    }
    public void OnApplicationQuit()
    {
        //Debug.Log("Đã lưu data");
        //saveData(listOfKPI);
    }
}
