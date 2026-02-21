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



# Conclusion & Analysis

---

## 1) Overall Performance

จากค่าเฉลี่ยของตัวชี้วัดทั้ง **Internal (Silhouette)** และ **External (ARI, NMI)** บนทุกชุดข้อมูล พบว่า **Cosine distance ให้ผลลัพธ์ดีที่สุดโดยรวม**

### Average Silhouette Score
- **Cosine:** 0.552  
- Manhattan: 0.413  
- Euclidean: 0.401  

### Average External Metrics (เฉพาะ dataset ที่มี label)
- **Cosine:**  
  - NMI = 0.459  
  - ARI = 0.394  

Cosine ให้คะแนนสูงกว่า Euclidean และ Manhattan ประมาณ 20–30% โดยเฉลี่ย

ผลลัพธ์นี้สะท้อนว่าในหลายชุดข้อมูล (โดยเฉพาะ synthetic datasets ที่มีโครงสร้างเชิงรูปแบบชัดเจน) การพิจารณา **ทิศทางของเวกเตอร์ (angle-based similarity)** มีประสิทธิภาพมากกว่าการวัดระยะเชิงเส้นเพียงอย่างเดียว

อย่างไรก็ตาม ความแตกต่างนี้เป็นผลจากค่าเฉลี่ย across datasets และไม่ได้หมายความว่า Cosine จะเหนือกว่าในทุกบริบท

---

## 2) Notable Exceptions & the “No Free Lunch” Insight

แม้ Cosine จะมี performance โดยรวมดีที่สุด แต่มีบางชุดข้อมูลที่ metric อื่นทำได้ดีกว่าอย่างชัดเจน

### Finance_data (Real-world, ไม่มี label)

- Manhattan Silhouette = 0.391  
- Cosine Silhouette = 0.211  

Manhattan ทำได้ดีกว่าเกือบเท่าตัว

**Interpretation:**  
ข้อมูลการเงินมักมีลักษณะ non-Gaussian distribution และมี outliers สูง ซึ่ง **L1 norm (Manhattan distance)** มีความ robust ต่อ outliers มากกว่า

---

### Campus_placement_data (Real-world)

- Euclidean Silhouette = 0.224  
- Cosine Silhouette = 0.082  

ข้อมูลชุดนี้ประกอบด้วยคะแนนสอบและตัวแปรเชิงปริมาณที่ “ขนาดมีความหมาย” (magnitude-sensitive features)  
การวัดมุมแบบ Cosine จึงสูญเสียข้อมูลเชิงขนาด ทำให้ผลลัพธ์ด้อยกว่า Euclidean อย่างชัดเจน

---

## 3) Internal vs External: A Critical Observation

ผลการทดลองแสดงให้เห็นว่า:

> ค่า Silhouette ที่สูง ไม่ได้แปลว่าการจัดกลุ่มถูกต้องตาม Ground Truth เสมอไป

ตัวอย่างเช่น ในบางชุดข้อมูล real-world แม้ Cosine จะให้ค่า Silhouette สูงมาก (เช่น 0.743 ใน iris) แต่ค่า ARI กลับต่ำ (0.048)

กรณีนี้สะท้อนว่าโมเดลสามารถสร้างกลุ่มที่ “ชัดในเชิงเรขาคณิต” ได้ดี  
แต่โครงสร้างเชิงเรขาคณิตนั้นอาจไม่สอดคล้องกับ label จริงของข้อมูล

ดังนั้นการประเมิน clustering ควรใช้ทั้ง internal และ external metrics ร่วมกันเสมอ

---

# Final Verdict

- **Default choice:** Cosine distance เป็นตัวเลือกที่เหมาะสมสำหรับงาน clustering ทั่วไป โดยเฉพาะข้อมูลที่ pattern เชิงทิศทางมีความสำคัญ  
- **Magnitude-based data (เช่น คะแนน/ปริมาณเชิงกายภาพ):** Euclidean มักให้ผลลัพธ์ดีกว่า  
- **High-noise / outlier-heavy datasets:** Manhattan เป็นทางเลือกที่ robust และเสถียรกว่า  

ผลการทดลองนี้ยืนยันว่า  

> **ไม่มี distance metric ใดที่ดีที่สุดสำหรับทุกปัญหา (No Free Lunch Principle)**  

การเลือก metric ควรพิจารณาจากลักษณะเชิงสถิติและความหมายของ features ในแต่ละ dataset
