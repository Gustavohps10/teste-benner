import { api } from '@/lib/axios'

export type UpdateHeatingProgramRequest = {
  id: number
  name: string
  food: string
  time: number
  power: number
  heatingChar: string
  instructions?: string
}

export async function updateHeatingProgram({
  id,
  name,
  food,
  power,
  time,
  heatingChar,
  instructions,
}: UpdateHeatingProgramRequest) {
  await api.put('/programs', {
    id,
    name,
    food,
    power,
    time,
    heatingChar,
    instructions,
  })
}
