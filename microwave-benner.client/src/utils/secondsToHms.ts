export function secondsToHms(duration: number): string {
  const hours = Math.floor(duration / 3600)
  const minutes = Math.floor((duration % 3600) / 60)
  const seconds = Math.floor((duration % 3600) % 60)

  const hourDisplay = (hours < 10 ? '0' : '') + hours
  const minutsDisplay = (minutes < 10 ? '0' : '') + minutes
  const secondsDisplay = (seconds < 10 ? '0' : '') + seconds

  if (hours < 1) {
    return minutsDisplay + ':' + secondsDisplay
  }
  return hourDisplay + ':' + minutsDisplay + ':' + secondsDisplay
}
