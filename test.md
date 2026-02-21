# Experiment 2: Effect of Distance Metrics & Linkage in Agglomerative Clustering

---

## Objective (วัตถุประสงค์)

การทดลองนี้มีวัตถุประสงค์เพื่อศึกษาผลกระทบของ **มาตรวัดระยะทาง (Distance Metrics)** และ **วิธีการเชื่อมโยงคลัสเตอร์ (Linkage Methods)** ต่อประสิทธิภาพของการจัดกลุ่มข้อมูลด้วย **Agglomerative Clustering**  

โดยทำการเปรียบเทียบระหว่าง **Euclidean, Manhattan และ Cosine distance** ภายใต้ **Average และ Complete linkage** บนชุดข้อมูลหลายประเภท ทั้ง **Synthetic datasets** และ **Real-world datasets**

นอกจากนี้ การทดลองนี้ยังมุ่งศึกษาผลกระทบเชิงปฏิสัมพันธ์ (*interaction effect*) ระหว่าง distance metric และ linkage strategy แทนการประเมิน metric แบบแยกเดี่ยว เพื่อทำความเข้าใจว่าการจับคู่ระหว่าง metric และ linkage ส่งผลต่อโครงสร้างของคลัสเตอร์อย่างไร

---

## Experimental Design (การออกแบบการทดลอง)

### A) Algorithm & Preprocessing (อัลกอริทึมและการเตรียมข้อมูล)

- **Algorithm:** ใช้ Agglomerative Clustering (Hierarchical Clustering)
- **Configurations Tested:** ทดสอบทั้งหมด 3 Distance Metrics × 2 Linkages รวมเป็น **6 รูปแบบการทดลอง**
  - Distance Metrics: `euclidean`, `manhattan`, `cosine`
  - Linkage Methods: `average`, `complete`
- **Scaling:** ทุกชุดข้อมูลผ่านการปรับมาตรฐานด้วย `StandardScaler` ($\mu = 0,\ \sigma = 1$) เพื่อลดผลกระทบจากความแตกต่างของสเกล ซึ่งมีผลโดยตรงต่อการคำนวณระยะทาง
- **Sampling (Memory Constraint):** หากชุดข้อมูลมีขนาดมากกว่า 30,000 แถว จะทำการสุ่มตัวอย่างแบบ Random Downsampling เหลือ 30,000 แถว โดยกำหนด `random_state = 42` เพื่อให้ผลการทดลองสามารถทำซ้ำได้ (*reproducibility*)

---

### B) Variables (ตัวแปรในการทดลอง)

- **Independent Variables (ตัวแปรต้น):**
  - ประเภทของ Distance Metric
  - ประเภทของ Linkage Method

- **Controlled Variable (ตัวแปรควบคุม):**
  - กำหนดจำนวนคลัสเตอร์คงที่ที่ $K = 3$ สำหรับทุกการทดลอง  
    เพื่อควบคุมความซับซ้อนของโมเดล และมุ่งเน้นศึกษาผลกระทบจาก metric และ linkage โดยตรง  

  *หมายเหตุ:* การกำหนด $K = 3$ ช่วยควบคุมตัวแปรในการเปรียบเทียบ แต่บาง dataset อาจไม่ได้มีโครงสร้างที่เหมาะสมที่สุดที่ 3 คลัสเตอร์ ดังนั้นค่าดังกล่าวอาจไม่ใช่ optimal number of clusters สำหรับทุกชุดข้อมูล

---

### C) Evaluation Metrics (ตัวชี้วัดประสิทธิภาพ)

การประเมินผลแบ่งออกเป็น 2 มิติหลัก ได้แก่

- **Internal Validation**
  - ใช้ **Silhouette Score** (ช่วงค่า -1 ถึง 1) เพื่อวัดความหนาแน่นภายในกลุ่ม (cohesion) และระยะห่างระหว่างกลุ่ม (separation)

- **External Validation** (เฉพาะชุดข้อมูลที่มี Ground Truth Labels)
  - **Adjusted Rand Index (ARI)**
  - **Normalized Mutual Information (NMI)**

การใช้ทั้ง internal และ external metrics ร่วมกันช่วยให้สามารถประเมินคุณภาพของ clustering ได้ทั้งในเชิงโครงสร้างเชิงเรขาคณิตของข้อมูล และในเชิงความสอดคล้องกับ label จริงของข้อมูล
