import { api } from '@/lib/axios'

export async function deleteHeatingProgram(id: number) {
  await api.delete(`/programs/${id}`)
}
