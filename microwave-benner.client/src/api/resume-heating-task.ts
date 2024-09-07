import { api } from '@/lib/axios'

export type ResumeHeatingTaskResponse = {
    id: number,
    time: number,
    power: number,
    startTime: string,
    pauseTime: string,
    endTime: string,
    heatingProgramId: number | null
  }

export async function resumeHeatingTask(id: number) {
    const response = await api.post<ResumeHeatingTaskResponse>(`/heatings/resume/${id}`)
    return response.data
}