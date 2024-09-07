import { api } from '@/lib/axios'

export async function resumeHeatingTask(id: number) {
    await api.post(`/heatings/resume/${id}`)
}