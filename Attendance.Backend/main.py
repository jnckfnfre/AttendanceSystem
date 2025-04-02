from fastapi import FastAPI, Request
from pydantic import BaseModel
from fastapi.middleware.cors import CORSMiddleware

app = FastAPI()

# Allow Blazor frontend to call this API
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # Or set to "http://localhost:5168" to restrict
    allow_methods=["*"],
    allow_headers=["*"],
)

# In-memory store to mimic a database
attendance_log = []

# ---------- DATA MODELS ----------
class AuthPayload(BaseModel):
    utdId: str
    password: str

class SubmissionPayload(BaseModel):
    utdId: str
    password: str
    answer1: str = None
    answer2: str = None
    answer3: str = None

# ---------- ROUTES ----------
@app.post("/api/auth")
def auth(data: AuthPayload):
    if data.password == "csdemo123":
        return {"status": "authorized"}
    return {"status": "unauthorized"}

@app.post("/api/attendance")
async def submit_attendance(request: Request, data: SubmissionPayload):
    client_ip = request.client.host  # Capture student's IP
    attendance_log.append({
        "utdId": data.utdId,
        "ip": client_ip,
        "answers": [data.answer1, data.answer2, data.answer3]
    })
    return {"message": "Attendance recorded", "ip": client_ip}

@app.get("/api/attendance")
def get_attendance():
    return {"records": attendance_log}
