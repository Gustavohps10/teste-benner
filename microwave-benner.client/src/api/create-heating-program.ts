import { api } from '@/lib/axios'

export type CreateHeatingProgramRequest = {
  name: string
  food: string
  time: number
  power: number
  heatingChar: string
  instructions?: string
}

export async function createHeatingProgram({
  name,
  food,
  power,
  time,
  heatingChar,
  instructions,
}: CreateHeatingProgramRequest) {
  await api.post('/programs', {
    name,
    food,
    power,
    time,
    heatingChar,
    instructions,
  })
}
