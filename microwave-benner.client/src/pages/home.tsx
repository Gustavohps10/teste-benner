import { useEffect, useState } from 'react'

import { Button } from '@/components/ui/button'
import { Slider } from '@/components/ui/slider'
import { secondsToHms } from '@/utils/secondsToHms'
import { getPrograms } from '@/api/get-programs'
import { useQuery, useQueryClient } from '@tanstack/react-query'

export function Home() {
  const { data: result } = useQuery({ queryKey: ['programs'], queryFn: getPrograms })
  console.log(result);
  
  
  const [time, setTime] = useState(0)
  const [textTime, setTextTime] = useState('')
  const [power, setPower] = useState([5])
  const [isRunning, setIsRunning] = useState(false)
  const [isPaused, setIsPaused] = useState(false)

  useEffect(() => {
    let timer: NodeJS.Timeout
    if (isRunning && !isPaused) {
      timer = setInterval(() => {
        setTime((state) => {
          if (state <= 1) {
            clearInterval(timer)
            setIsRunning(false)
            setTextTime('')
            return 0
          }
          return state - 1
        })
      }, 1000)
    }

    return () => clearInterval(timer)
  }, [isRunning, isPaused])

  function writeOnVisor(digit: number) {
    if (!isRunning && !isPaused) {
      setTextTime((state) => state + digit)
    }
  }

  function handleStartHeating() {
    if (isPaused) {
      setIsRunning(true)
      setIsPaused(false)
      return
    }

    if (isRunning) return

    const parsedTime = Number(textTime)
    if (parsedTime > 0) {
      setTime(parsedTime)
      setIsRunning(true)
    }
  }

  function handleCancelOrPause() {
    if (isRunning && !isPaused) {
      setIsPaused(true)
      setIsRunning(false)
      return
    }

    if (isPaused || (!isRunning && textTime)) {
      setIsRunning(false)
      setIsPaused(false)
      setTime(0)
      setTextTime('')
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
            {isRunning || isPaused ? secondsToHms(time) : textTime}
          </span>
        </div>

        <div className="grid grid-cols-3 justify-center gap-3 py-4">
          {Array.from({ length: 9 }).map((_, index) => (
            <Button
              key={index}
              size="lg"
              variant="secondary"
              onClick={() => writeOnVisor(index + 1)}
            >
              {index + 1}
            </Button>
          ))}
          <Button size="lg" variant="secondary" onClick={() => writeOnVisor(0)}>
            0
          </Button>
          <Button size="lg" variant="outline" onClick={handleStartHeating}>
            Play
          </Button>
          <Button size="lg" variant="destructive" onClick={handleCancelOrPause}>
            {isPaused ? 'Cancel' : 'Pause'}
          </Button>
        </div>

        <div>
          <span className="text-muted-foreground text-sm">Potência</span>
          <Slider
            className="mt-2"
            min={1}
            max={10}
            value={power}
            onValueChange={setPower}
          />
        </div>
      </div>
    </div>
  )
}
