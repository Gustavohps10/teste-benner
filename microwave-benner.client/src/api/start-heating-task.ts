import { api } from '@/lib/axios'

export type StartHeatingTaskRequest = {
  time?: number
  power?: number
  heatingProgramId?: number
}

export type StartHeatingTaskResponse = {
  id: number
  time: number
  power: number
  startTime: string
  pauseTime: string
  endTime: string
  heatingProgramId: number | null
}

export async function startHeatingTask({
  power,
  time,
  heatingProgramId,
}: StartHeatingTaskRequest) {
  const response = await api.post<StartHeatingTaskResponse>('/heatings', {
    power,
    time,
    heatingProgramId,
  })
  return response.data
}
