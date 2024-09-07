import { api } from '@/lib/axios'

export type GetProgramsResponse = {
  id: number
  name: string
  food: string
  time: number
  power: number
  heatingChar:string
  instructions: string
  custom: boolean
}[]

export async function getPrograms() {
  const response = await api.get<GetProgramsResponse>('/programs')
  return response.data
}