import { useMutation, useQuery } from '@tanstack/react-query'
import { useEffect, useReducer, useState } from 'react'

import { Program } from '@/@types/Program'
import { addTimeToHeatingTask } from '@/api/add-time-to-heating-task'
import { getPrograms, GetProgramsResponse } from '@/api/get-programs'
import { pauseOrCancelHeatingTask } from '@/api/pause-or-cancel-heating-task'
import { resumeHeatingTask } from '@/api/resume-heating-task'
import { startHeatingTask } from '@/api/start-heating-task'
import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog'
import { Slider } from '@/components/ui/slider'
import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { heatingTaskReducer } from '@/reducers/heatingTask'
import { secondsToHms } from '@/utils/secondsToHms'

export function Home() {
  const [state, dispatch] = useReducer(heatingTaskReducer, {
    power: 10,
    time: 0,
    isRunning: false,
    isPaused: false,
  })
  console.log(state)
  const [textTime, setTextTime] = useState('')
  const [infoString, setInfoString] = useState('')
  const [heatingChar, setHeatingChar] = useState('.')

  const { mutateAsync: startHeatingTaskFn, isPending: isStarting } =
    useMutation({
      mutationFn: startHeatingTask,
    })

  const {
    mutateAsync: pauseOrCancelHeatingTaskFn,
    isPending: isPausingOrCancelling,
  } = useMutation({
    mutationFn: pauseOrCancelHeatingTask,
  })

  const { mutateAsync: resumeHeatingTaskFn } = useMutation({
    mutationFn: resumeHeatingTask,
  })

  const { mutateAsync: addTimeToHeatingTaskFn } = useMutation({
    mutationFn: addTimeToHeatingTask,
  })

  const { data: programs } = useQuery<GetProgramsResponse>({
    queryKey: ['programs'],
    queryFn: getPrograms,
  })

  useEffect(() => {
    let timer: NodeJS.Timeout
    if (state.isRunning && !state.isPaused) {
      timer = setInterval(() => {
        dispatch({ type: 'SET_TIME', payload: state.time - 1 })
        if (state.time <= 1) {
          clearInterval(timer)
          dispatch({ type: 'STOP' })
          setInfoString('Aquecimento concluído')
          setTextTime('')
        } else {
          const charGroup = heatingChar.repeat(state.power) + ' '
          const formatedChars = charGroup.repeat(state.time - 1)
          setInfoString(formatedChars)
        }
      }, 1000)
    }

    return () => clearInterval(timer)
  }, [state.isRunning, state.isPaused, state.time, state.power, heatingChar])

  async function handleStartHeating(programId?: number) {
    if (programId && !state.isRunning && !state.isPaused) {
      try {
        const startheatingTaskResponse = await startHeatingTaskFn({
          heatingProgramId: programId,
        })
        dispatch({ type: 'SET_TASK', payload: startheatingTaskResponse })
        dispatch({ type: 'START' })
      } catch (err) {
        console.error(
          'Erro ao iniciar aquecimento com programa de pré aquecimento:',
          err,
        )
      }

      return
    }

    if (state.isPaused) {
      if (state.id) {
        try {
          await resumeHeatingTaskFn(state.id)
          dispatch({ type: 'RESUME' })
        } catch (err) {
          console.error('Erro ao retomar aquecimento:', err)
        }
      }
      return
    }

    if (state.isRunning) {
      if (state.id) {
        try {
          await addTimeToHeatingTaskFn(state.id)
          dispatch({ type: 'SET_TIME', payload: state.time + 30 }) // Aqui adiciona 30 segundos
        } catch (err) {
          console.error('Erro ao adicionar tempo ao aquecimento:', err)
        }
      }
      return
    }

    const parsedTime = Number(textTime)
    if (parsedTime > 0) {
      dispatch({ type: 'SET_TIME', payload: parsedTime })

      try {
        const startheatingTaskResponse = await startHeatingTaskFn({
          power: state.power,
          time: parsedTime,
        })

        dispatch({ type: 'SET_TASK', payload: startheatingTaskResponse })
        dispatch({ type: 'START' })
      } catch (err) {
        console.error('Erro ao iniciar aquecimento:', err)
      }
    } else {
      try {
        const startheatingTaskResponse = await startHeatingTaskFn({})

        dispatch({ type: 'SET_TASK', payload: startheatingTaskResponse })
        dispatch({ type: 'START' })
      } catch (err) {
        console.error('Erro ao iniciar aquecimento com valores padrão:', err)
      }
    }
  }

  async function handleCancelOrPause() {
    if (!state.id) {
      setTextTime('')
      return
    }

    try {
      if (state.isRunning && !state.isPaused) {
        await pauseOrCancelHeatingTaskFn(state.id)
        dispatch({ type: 'PAUSE' })
        return
      }

      if (state.isPaused || (!state.isRunning && textTime)) {
        await pauseOrCancelHeatingTaskFn(state.id)
        dispatch({ type: 'STOP' })
        setTextTime('')
        setHeatingChar('.')
        setInfoString('')
      }
    } catch (err) {
      console.error('Erro ao pausar ou cancelar aquecimento:', err)
    }
  }

  function handleChangePower(value: number) {
    dispatch({ type: 'SET_POWER', payload: value })
  }

  function handleDigitClick(digit: number) {
    if (!state.isRunning && !state.isPaused) {
      setTextTime((prev) => prev + digit)
    }
  }

  function handleProgramSelection(program: Program) {
    if (!state.isRunning) {
      setHeatingChar(program.heatingChar)
      handleStartHeating(program.id)
    }
  }

  return (
    <div className="border rounded-md w-[900px] h-[500px] mx-auto my-20 flex">
      <div className="h-full flex-1 p-6">
        <div className="border w-full h-full rounded-md p-4 overflow-y-auto">
          <span className="font-bold text-neutral-500 dark:text-neutral-200 text-2xl">
            {infoString}
          </span>
        </div>
      </div>

      <div className="border-l w-[280px] p-6">
        <div className="border rounded-md w-full h-24 flex items-center justify-center">
          <span className="font-bold text-neutral-500 dark:text-neutral-200 text-4xl">
            {state.isRunning || state.isPaused
              ? secondsToHms(state.time)
              : textTime}
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
          <Button
            size="lg"
            variant="secondary"
            onClick={() => handleDigitClick(0)}
          >
            0
          </Button>
          <Button
            size="lg"
            variant="outline"
            onClick={() => handleStartHeating()}
            disabled={isStarting}
          >
            {isStarting ? 'Starting...' : state.isPaused ? 'Resume' : 'Play'}
          </Button>
          <Button
            size="lg"
            variant="destructive"
            onClick={handleCancelOrPause}
            disabled={isPausingOrCancelling}
          >
            {isPausingOrCancelling
              ? 'Pausing/Canceling...'
              : state.isPaused
                ? 'Cancel'
                : 'Pause'}
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

        <Dialog>
          <DialogTrigger asChild>
            <Button className="mt-4" variant="outline">
              Pré-configurados
            </Button>
          </DialogTrigger>
          <DialogContent
            className={'lg:max-w-screen-lg overflow-y-scroll max-h-[95vh]'}
          >
            <DialogHeader>
              <DialogTitle>Programas de aquecimento</DialogTitle>
              <DialogDescription className="font-semibold">
                Selecione um programa de aquecimento
                <Table>
                  <TableCaption>
                    Lista de programas pré-configurados.
                  </TableCaption>
                  <TableHeader>
                    <TableRow>
                      <TableHead className="w-[48px]">ID</TableHead>
                      <TableHead>Nome</TableHead>
                      <TableHead>Tempo</TableHead>
                      <TableHead>Potência</TableHead>
                      <TableHead>Instruções</TableHead>
                      <TableHead></TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody>
                    {programs?.map((program) => (
                      <TableRow key={program.id}>
                        <TableCell className="font-sm font-mono w-[100px] font-medium">
                          {program.id}
                        </TableCell>
                        <TableCell className="text-secondary-foreground">
                          {program.name}
                        </TableCell>
                        <TableCell className="font-sm font-mono">
                          {secondsToHms(program.time)}
                        </TableCell>
                        <TableCell>{program.power}</TableCell>
                        <TableCell>{program.instructions}</TableCell>
                        <TableCell>
                          <DialogClose asChild>
                            <Button
                              onClick={() => handleProgramSelection(program)}
                            >
                              Selecionar
                            </Button>
                          </DialogClose>
                        </TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </DialogDescription>
            </DialogHeader>
          </DialogContent>
        </Dialog>
      </div>
    </div>
  )
}
