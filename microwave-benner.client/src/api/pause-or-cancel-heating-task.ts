import { api } from '@/lib/axios'

export async function pauseOrCancelHeatingTask(id: number) {
    await api.post('/heatings/pause', {
    id
  })
}