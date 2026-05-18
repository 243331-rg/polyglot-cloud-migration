import time
import requests
import random  # Naya library add ki hai
import os      # Worker ID identify karne ke liye

# Backend URL
BACKEND_URL = "http://backend:8080/migration"

# Hum container ka ID le rahe hain taakay pata chale kaunsa worker kaam kar raha hai
WORKER_ID = os.uname()[1] 

def process_migration():
    try:
        print(f"🔍 [{WORKER_ID}] Checking for pending tasks...")
        response = requests.get(f"{BACKEND_URL}/next-task", timeout=5)
        
        if response.status_code == 200:
            task = response.json()
            
            if task and 'id' in task:
                task_id = task['id']
                file_name = task['fileName']
                
                # --- WEEK 2 UPDATES ---
                # 1. Random Processing Time (5 se 15 seconds)
                # Ye simulate karega ke bari file zyada time le rahi hai
                processing_time = random.randint(5, 15)
                
                print(f"🚀 [{WORKER_ID}] Found Task ID: {task_id} | File: {file_name}")
                print(f"⚙️ Migrating... will take {processing_time} seconds.")
                
                # 2. Simulation (Wait for random time)
                time.sleep(processing_time) 
                
                # 3. Notify Backend
                update_res = requests.post(f"{BACKEND_URL}/complete/{task_id}", timeout=5)
                
                if update_res.status_code == 200:
                    print(f"✅ [{WORKER_ID}] Task {task_id} COMPLETED.")
                else:
                    print(f"⚠️ [{WORKER_ID}] Update failed (Status: {update_res.status_code})")
            else:
                print(f"😴 [{WORKER_ID}] No pending tasks. Resting...")
        
        elif response.status_code == 404:
            print(f"😴 [{WORKER_ID}] Database empty.")
        else:
            print(f"❌ Backend error: {response.status_code}")

    except requests.exceptions.ConnectionError:
        print("🚫 Connection Error: Backend is down.")
    except Exception as e:
        print(f"⚠️ Error: {e}")

if __name__ == "__main__":
    print(f"========================================")
    print(f"🐍 Worker [{WORKER_ID}] is now ACTIVE")
    print(f"========================================")
    
    while True:
        process_migration()
        print("-" * 40)
        # Check intervals ko thora kam kar dete hain taakay scaling nazar aaye
        time.sleep(3)