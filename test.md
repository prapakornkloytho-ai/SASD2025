# Experiment 2: Effect of Distance Metrics & Linkage in Agglomerative Clustering

---

## Objective (วัตถุประสงค์)
การทดลองนี้ศึกษาผลกระทบของ **มาตรวัดระยะทาง (Distance Metrics)** และ **วิธีการเชื่อมโยง (Linkage)** ต่อประสิทธิภาพของการจัดกลุ่มข้อมูลด้วย **Agglomerative Clustering**  
โดยเปรียบเทียบ **Euclidean / Manhattan / Cosine** ภายใต้ **Average vs Complete linkage** บนชุดข้อมูลหลายประเภท (Synthetic และ Real-world)

---

## Experimental Design (การออกแบบการทดลอง)

### A) Algorithm & Preprocessing (อัลกอริทึมและการเตรียมข้อมูล)
- **Algorithm:** Agglomerative Clustering  
- **Configurations Tested:** 3 Distance Metrics × 2 Linkages = **6 รูปแบบการทดลอง**
  - Distance Metrics: `euclidean`, `manhattan`, `cosine`
  - Linkage: `average`, `complete`
- **Scaling:** ใช้ `StandardScaler` ($\mu=0,\ \sigma=1$) กับทุกชุดข้อมูล เพื่อลดผลกระทบจากสเกลที่ต่างกัน
- **Sampling (Memory Constraint):** หากข้อมูลมากกว่า 30,000 แถว จะสุ่ม downsample เหลือ 30,000 ด้วย `random_state=42` เพื่อความทำซ้ำได้ (reproducibility)

### B) Variables (ตัวแปรในการทดลอง)
- **Independent Variables:** Distance metric และ Linkage method
- **Controlled Variable:** จำนวนคลัสเตอร์คงที่ที่ $K=3$ ทุกการทดลอง เพื่อโฟกัสผลกระทบจาก metric/linkage
  - *หมายเหตุ:* การ fix $K=3$ ช่วยควบคุมตัวแปร แต่บาง dataset อาจไม่ได้มีโครงสร้างตาม 3 กลุ่มจริง จึงอาจไม่ใช่ค่าที่เหมาะที่สุดสำหรับทุกชุดข้อมูล

### C) Evaluation Metrics (ตัวชี้วัด)
- **Internal Validation:** Silhouette Score (ช่วง -1 ถึง 1) วัด cohesion/separation ของคลัสเตอร์
- **External Validation (เฉพาะชุดที่มี label):**
  - Adjusted Rand Index (ARI)
  - Normalized Mutual Information (NMI)

---
