import { addTimeToHeatingTask } from '@/api/add-time-to-heating-task';
import { pauseOrCancelHeatingTask } from '@/api/pause-or-cancel-heating-task';
import { resumeHeatingTask } from '@/api/resume-heating-task';
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

  const { mutateAsync: startHeatingTaskFn, isPending: isStarting, isError: startError, error: startErrorMsg } = useMutation({
    mutationFn: startHeatingTask,
  });

  const { mutateAsync: pauseOrCancelHeatingTaskFn, isPending: isPausingOrCancelling, isError: pauseError, error: pauseErrorMsg } = useMutation({
    mutationFn: pauseOrCancelHeatingTask,
  });

  const { mutateAsync: resumeHeatingTaskFn, isPending: isResuming, isError: resumeError, error: resumeErrorMsg } = useMutation({
    mutationFn: resumeHeatingTask,
  });
  
  const { mutateAsync: addTimeToHeatingTaskFn, isPending: isAddingTime, isError: addTimeError, error: addTimeErrorMsg } = useMutation({
    mutationFn: addTimeToHeatingTask,
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
      if (state.id) {
        try {
          await resumeHeatingTaskFn(state.id);
          dispatch({ type: 'RESUME' });
        } catch (err) {
          console.error('Erro ao retomar aquecimento:', err);
        }
      }
      return;
    }

    if (state.isRunning) {
      // Adiciona 30 segundos ao tempo restante
      if (state.id) {
        try {
          await addTimeToHeatingTaskFn(state.id); // Certifique-se de que addTimeToHeatingTaskFn está definido
          dispatch({ type: 'SET_TIME', payload: state.time + 30 });
        } catch (err) {
          console.error('Erro ao adicionar tempo ao aquecimento:', err);
        }
      }
      return;
    }

    const parsedTime = Number(textTime);
    if (parsedTime > 0) {
      dispatch({ type: 'SET_TIME', payload: parsedTime });

      try {
        const startheatingTaskResponse = await startHeatingTaskFn({
          power: state.power,
          time: parsedTime,
        });

        dispatch({ type: 'SET_TASK', payload: startheatingTaskResponse });
        dispatch({ type: 'START' });
      } catch (err) {
        console.error('Erro ao iniciar aquecimento:', err);
      }
    }
  }

  async function handleCancelOrPause() {
    if (!state.id) {
      setTextTime('');
      return;
    }

    try {
      if (state.isRunning && !state.isPaused) {
        await pauseOrCancelHeatingTaskFn(state.id);
        dispatch({ type: 'PAUSE' });
        return;
      }

      if (state.isPaused || (!state.isRunning && textTime)) {
        await pauseOrCancelHeatingTaskFn(state.id); 
        dispatch({ type: 'STOP' });
        setTextTime('');
      }
    } catch (err) {
      console.error('Erro ao pausar ou cancelar aquecimento:', err);
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
          <Button size="lg" variant="outline" onClick={handleStartHeating} disabled={isStarting}>
            {isStarting ? 'Starting...' : state.isPaused ? 'Resume' : 'Play'}
          </Button>
          <Button size="lg" variant="destructive" onClick={handleCancelOrPause} disabled={isPausingOrCancelling}>
            {isPausingOrCancelling ? 'Pausing/Canceling...' : state.isPaused ? 'Cancel' : 'Pause'}
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

        {startError && <div className="text-red-500 mt-4">Error: {startErrorMsg instanceof Error ? startErrorMsg.message : 'Unknown error'}</div>}
        {pauseError && <div className="text-red-500 mt-4">Error: {pauseErrorMsg instanceof Error ? pauseErrorMsg.message : 'Unknown error'}</div>}
        {resumeError && <div className="text-red-500 mt-4">Error: {resumeErrorMsg instanceof Error ? resumeErrorMsg.message : 'Unknown error'}</div>}
      </div>
    </div>
  );
}
