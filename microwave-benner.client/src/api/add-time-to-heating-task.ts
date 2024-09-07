import { api } from '@/lib/axios'

export type AddTimeToHeatingTask = {
  id: number,
  time: number,
  power: number,
  startTime: string,
  pauseTime: string,
  endTime: string,
  heatingProgramId: number | null
}

export async function addTimeToHeatingTask(id: number) {
  const response = await api.post<AddTimeToHeatingTask>(`/heatings/${id}/add-time`)
  return response.data
}