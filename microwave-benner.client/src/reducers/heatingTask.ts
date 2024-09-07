interface HeatingTask {
    power: number;
    time: number;
    isRunning: boolean;
    isPaused: boolean;
}

type HeatingTaskAction =
  | { type: 'START' }
  | { type: 'PAUSE' }
  | { type: 'RESUME' }
  | { type: 'STOP' }
  | { type: 'SET_TIME'; payload: number }
  | { type: 'SET_POWER'; payload: number };

export function heatingTaskReducer(state: HeatingTask, action: HeatingTaskAction): HeatingTask {
  switch (action.type) {
    case 'START':
      return { ...state, isRunning: true, isPaused: false };
    case 'PAUSE':
      return { ...state, isRunning: false, isPaused: true };
    case 'RESUME':
      return { ...state, isRunning: true, isPaused: false };
    case 'STOP':
      return { ...state, isRunning: false, isPaused: false, time: 0 };
    case 'SET_TIME':
      return { ...state, time: action.payload };
    case 'SET_POWER':
      return { ...state, power: action.payload };
    default:
      return state;
  }
}