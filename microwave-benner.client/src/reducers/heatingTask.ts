interface HeatingTask {
  id?: number
  power: number
  time: number
  programId?: number
  isRunning?: boolean
  isPaused?: boolean
}

type HeatingTaskAction =
  | { type: 'START' }
  | { type: 'PAUSE' }
  | { type: 'RESUME' }
  | { type: 'STOP' }
  | { type: 'SET_TIME'; payload: number }
  | { type: 'SET_POWER'; payload: number }
  | { type: 'SET_PROGRAM_ID'; payload: number }
  | { type: 'SET_TASK'; payload: HeatingTask }

export function heatingTaskReducer(
  state: HeatingTask,
  action: HeatingTaskAction,
): HeatingTask {
  switch (action.type) {
    case 'START':
      return { ...state, isRunning: true, isPaused: false }
    case 'PAUSE':
      return { ...state, isRunning: false, isPaused: true }
    case 'RESUME':
      return { ...state, isRunning: true, isPaused: false }
    case 'STOP':
      return { ...state, isRunning: false, isPaused: false, time: 0 }
    case 'SET_TIME':
      return { ...state, time: action.payload }
    case 'SET_POWER':
      return { ...state, power: action.payload }
    case 'SET_TASK':
      return {
        ...state,
        id: action.payload.id,
        power: action.payload.power,
        time: action.payload.time,
        isRunning: action.payload.isRunning,
        isPaused: action.payload.isPaused,
      }
    default:
      return state
  }
}
