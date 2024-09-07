import { startHeatingTask } from '@/api/start-heating-task';
import { Button } from '@/components/ui/button';
import { Slider } from '@/components/ui/slider';
import { heatingTaskReducer } from '@/reducers/heatingTask';
import { secondsToHms } from '@/utils/secondsToHms';
import { useMutation } from '@tanstack/react-query';
import { useReducer, useEffect, useState } from 'react';

export function Home() {
  const [state, dispatch] = useReducer(heatingTaskReducer, {
    power: 10,
    time: 0,
    isRunning: false,
    isPaused: false,
  });
  const [textTime, setTextTime] = useState('');

  const { mutateAsync: startHeatingTaskFn, isPending, isError, error } = useMutation({
    mutationFn: startHeatingTask,
  });

  useEffect(() => {
    let timer: NodeJS.Timeout;
    if (state.isRunning && !state.isPaused) {
      timer = setInterval(() => {
        dispatch({ type: 'SET_TIME', payload: state.time - 1 });
        if (state.time <= 1) {
          clearInterval(timer);
          dispatch({ type: 'STOP' });
          setTextTime('');
        }
      }, 1000);
    }

    return () => clearInterval(timer);
  }, [state.isRunning, state.isPaused, state.time]);

  async function handleStartHeating() {
    if (state.isPaused) {
      dispatch({ type: 'RESUME' });
      return;
    }

    if (state.isRunning) return;

    const parsedTime = Number(textTime);
    if (parsedTime > 0) {
      dispatch({ type: 'SET_TIME', payload: parsedTime });

      try {
        const heatingTaskResponse =  await startHeatingTaskFn({
          power: state.power,
          time: parsedTime,
          // heatingProgramId: null, 
        });

        dispatch({ type: 'START' });
        console.log(heatingTaskResponse);
        
      } catch (err) {
        console.error('Erro ao iniciar aquecimento:', err);
      }
    }
  }

  function handleCancelOrPause() {
    if (state.isRunning && !state.isPaused) {
      dispatch({ type: 'PAUSE' });
      return;
    }

    if (state.isPaused || (!state.isRunning && textTime)) {
      dispatch({ type: 'STOP' });
      setTextTime('');
    }
  }

  function handleChangePower(value: number) {
    dispatch({ type: 'SET_POWER', payload: value });
  }

  function handleDigitClick(digit: number) {
    if (!state.isRunning && !state.isPaused) {
      setTextTime((prev) => prev + digit);
    }
  }

  return (
    <div className="border rounded-md w-[900px] h-[500px] mx-auto my-20 flex">
      <div className="h-full flex-1 p-6">
        <div className="border w-full h-full rounded-md"></div>
      </div>

      <div className="border-l w-[280px] p-6">
        <div className="border rounded-md w-full h-24 flex items-center justify-center">
          <span className="font-bold text-neutral-500 dark:text-neutral-200 text-4xl">
            {state.isRunning || state.isPaused ? secondsToHms(state.time) : textTime}
          </span>
        </div>

        <div className="grid grid-cols-3 justify-center gap-3 py-4">
          {Array.from({ length: 9 }).map((_, index) => (
            <Button
              key={index}
              size="lg"
              variant="secondary"
              onClick={() => handleDigitClick(index + 1)}
            >
              {index + 1}
            </Button>
          ))}
          <Button size="lg" variant="secondary" onClick={() => handleDigitClick(0)}>
            0
          </Button>
          <Button size="lg" variant="outline" onClick={handleStartHeating} disabled={isPending}>
            {isPending ? 'Starting...' : 'Play'}
          </Button>
          <Button size="lg" variant="destructive" onClick={handleCancelOrPause}>
            {state.isPaused ? 'Cancel' : 'Pause'}
          </Button>
        </div>

        <div>
          <span className="text-muted-foreground text-sm">Potência</span>
          <Slider
            className="mt-2"
            min={1}
            max={10}
            value={[state.power]}
            onValueChange={(value) => handleChangePower(value[0])}
          />
        </div>

        {isError && <div className="text-red-500 mt-4">Error: {error instanceof Error ? error.message : 'Unknown error'}</div>}
      </div>
    </div>
  );
}
