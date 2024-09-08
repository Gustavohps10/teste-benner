'use client'

import { zodResolver } from '@hookform/resolvers/zod'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { BsThreeDots } from 'react-icons/bs'
import { FaLock } from 'react-icons/fa'
import { z } from 'zod'

import { createHeatingProgram } from '@/api/create-heating-program'
import { deleteHeatingProgram } from '@/api/delete-heating-program'
import { getPrograms } from '@/api/get-programs'
import { updateHeatingProgram } from '@/api/update-heating-program'
import { Button } from '@/components/ui/button'
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from '@/components/ui/card'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { Textarea } from '@/components/ui/textarea'
import { toast } from '@/hooks/use-toast'
import { secondsToHms } from '@/utils/secondsToHms'

const programFormSchema = z.object({
  name: z
    .string()
    .min(2, { message: 'Nome deve ter no mínimo 2 caracteres' })
    .max(50, { message: 'Nome deve ter no máximo 50 caracteres' }),
  food: z
    .string()
    .min(2, { message: 'Alimento deve ter no mínimo 2 caracteres' })
    .max(50, { message: 'Alimento deve ter no máximo 50 caracteres' }),
  time: z.coerce
    .number()
    .min(1, { message: 'Tempo deve ser no mínimo 1 minuto' })
    .max(120, { message: 'Tempo deve ser no máximo 120 minutos' }),
  power: z.coerce
    .number()
    .int()
    .min(1, { message: 'Potência mínima é 1' })
    .max(10, { message: 'Potência máxima é 10' }),
  heatingChar: z
    .string()
    .min(1, { message: 'Caracter de aquecimento obrigatório' })
    .max(1, { message: 'Somente 1 caracter permitido' }),
  instructions: z.string().max(255).optional(),
})

type ProgramForm = z.infer<typeof programFormSchema>

export function Programs() {
  const queryClient = useQueryClient()
  const [isCreateDialogOpen, setIsCreateDialogOpen] = useState(false)
  const [isEditDialogOpen, setIsEditDialogOpen] = useState(false)
  const [selectedProgramId, setSelectedProgramId] = useState<number | null>(
    null,
  )

  const { data: programs } = useQuery({
    queryKey: ['programs'],
    queryFn: getPrograms,
  })

  const form = useForm<ProgramForm>({
    resolver: zodResolver(programFormSchema),
    defaultValues: {
      name: '',
      food: '',
      time: 0,
      power: 0,
      heatingChar: '',
      instructions: '',
    },
  })

  const createMutation = useMutation({
    mutationFn: createHeatingProgram,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['programs'] })
      toast({ title: 'Programa criado com sucesso!', variant: 'default' })
      setIsCreateDialogOpen(false)
      form.reset()
    },
    onError: () => {
      toast({ title: 'Erro ao criar programa', variant: 'destructive' })
    },
  })

  const updateMutation = useMutation({
    mutationFn: updateHeatingProgram,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['programs'] })
      toast({ title: 'Programa atualizado com sucesso!', variant: 'default' })
      setIsEditDialogOpen(false)
      setSelectedProgramId(null)
      form.reset()
    },
    onError: () => {
      toast({ title: 'Erro ao atualizar programa', variant: 'destructive' })
    },
  })

  const deleteMutation = useMutation({
    mutationFn: deleteHeatingProgram,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['programs'] })
      toast({ title: 'Programa excluído com sucesso!', variant: 'default' })
    },
    onError: () => {
      toast({ title: 'Erro ao excluir programa', variant: 'destructive' })
    },
  })

  const handleCreateFormSubmit = (data: ProgramForm) => {
    createMutation.mutate(data)
  }

  const handleUpdateFormSubmit = (data: ProgramForm) => {
    if (selectedProgramId) {
      updateMutation.mutate({ id: selectedProgramId, ...data })
    }
  }

  const handleEdit = (program: ProgramForm, id: number) => {
    setSelectedProgramId(id)
    form.reset(program)
    setIsEditDialogOpen(true)
  }

  const handleDelete = (id: number) => {
    if (confirm('Tem certeza que deseja deletar este programa?')) {
      deleteMutation.mutate(id)
    }
  }

  const handleAddNew = () => {
    setIsCreateDialogOpen(true)
  }

  return (
    <div className="max-w-[90rem] mx-auto">
      <Card>
        <CardHeader>
          <CardTitle className="font-bold">Programas de aquecimento</CardTitle>
          <CardDescription>
            Você pode adicionar, remover, e alterar os programas
          </CardDescription>
        </CardHeader>
        <CardContent className="max-w-[70rem]">
          <Button className="mb-4" onClick={handleAddNew}>
            Adicionar
          </Button>

          <Dialog
            open={isCreateDialogOpen}
            onOpenChange={setIsCreateDialogOpen}
          >
            <DialogContent>
              <DialogHeader>
                <DialogTitle>Adicionar Programa</DialogTitle>
                <DialogDescription>
                  Preencha as informações do novo programa de aquecimento.
                </DialogDescription>
              </DialogHeader>
              <Form {...form}>
                <form onSubmit={form.handleSubmit(handleCreateFormSubmit)}>
                  <FormField
                    control={form.control}
                    name="name"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Nome</FormLabel>
                        <FormControl>
                          <Input id="name" placeholder="Nome" {...field} />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="food"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Alimento</FormLabel>
                        <FormControl>
                          <Input id="food" placeholder="Alimento" {...field} />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="time"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Tempo (minutos)</FormLabel>
                        <FormControl>
                          <Input
                            id="time"
                            type="number"
                            placeholder="Tempo"
                            min={1}
                            max={120}
                            {...field}
                          />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="power"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Potência</FormLabel>
                        <FormControl>
                          <Input
                            id="power"
                            type="number"
                            placeholder="Potência"
                            min={1}
                            max={10}
                            {...field}
                          />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="heatingChar"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Caracter de Aquecimento</FormLabel>
                        <FormControl>
                          <Input
                            id="heatingChar"
                            placeholder="Caracter"
                            max={1}
                            {...field}
                          />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="instructions"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Instruções</FormLabel>
                        <FormControl>
                          <Textarea
                            id="instructions"
                            placeholder="Instruções"
                            {...field}
                          />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <Button type="submit" className="mt-4">
                    Criar Programa
                  </Button>
                </form>
              </Form>
            </DialogContent>
          </Dialog>

          <Dialog open={isEditDialogOpen} onOpenChange={setIsEditDialogOpen}>
            <DialogContent>
              <DialogHeader>
                <DialogTitle>Editar Programa</DialogTitle>
                <DialogDescription>
                  Atualize as informações do programa de aquecimento.
                </DialogDescription>
              </DialogHeader>
              <Form {...form}>
                <form onSubmit={form.handleSubmit(handleUpdateFormSubmit)}>
                  <FormField
                    control={form.control}
                    name="name"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Nome</FormLabel>
                        <FormControl>
                          <Input id="name" placeholder="Nome" {...field} />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="food"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Alimento</FormLabel>
                        <FormControl>
                          <Input id="food" placeholder="Alimento" {...field} />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="time"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Tempo (minutos)</FormLabel>
                        <FormControl>
                          <Input
                            id="time"
                            type="number"
                            placeholder="Tempo"
                            {...field}
                          />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="power"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Potência</FormLabel>
                        <FormControl>
                          <Input
                            id="power"
                            type="number"
                            placeholder="Potência"
                            {...field}
                          />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="heatingChar"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Caracter de Aquecimento</FormLabel>
                        <FormControl>
                          <Input
                            id="heatingChar"
                            placeholder="Caracter"
                            {...field}
                          />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="instructions"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Instruções</FormLabel>
                        <FormControl>
                          <Textarea
                            id="instructions"
                            placeholder="Instruções"
                            {...field}
                          />
                        </FormControl>
                        <FormMessage className="text-red-500" />
                      </FormItem>
                    )}
                  />
                  <Button type="submit" className="mt-4">
                    Atualizar Programa
                  </Button>
                </form>
              </Form>
            </DialogContent>
          </Dialog>

          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Nome</TableHead>
                <TableHead>Alimento</TableHead>
                <TableHead>Tempo</TableHead>
                <TableHead>Potência</TableHead>
                <TableHead>Caracter</TableHead>
                <TableHead>Instruções</TableHead>
                <TableHead></TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {programs?.map((program) => (
                <TableRow key={program.id}>
                  <TableCell>{program.name}</TableCell>
                  <TableCell>{program.food}</TableCell>
                  <TableCell>{secondsToHms(program.time)}</TableCell>
                  <TableCell>{program.power}</TableCell>
                  <TableCell>{program.heatingChar}</TableCell>
                  <TableCell>{program.instructions}</TableCell>
                  <TableCell>
                    <DropdownMenu>
                      {!program.custom ? (
                        <FaLock className="mx-auto" />
                      ) : (
                        <DropdownMenuTrigger asChild>
                          <Button
                            variant="ghost"
                            size="icon"
                            className="mx-auto rounded-full text-muted-foreground hover:text-foreground"
                          >
                            <BsThreeDots />
                          </Button>
                        </DropdownMenuTrigger>
                      )}

                      <DropdownMenuContent>
                        <DropdownMenuLabel>Ações</DropdownMenuLabel>
                        <DropdownMenuSeparator />
                        <DropdownMenuItem
                          onClick={() => handleEdit(program, program.id)}
                        >
                          Editar
                        </DropdownMenuItem>
                        <DropdownMenuItem
                          onClick={() => handleDelete(program.id)}
                        >
                          Deletar
                        </DropdownMenuItem>
                      </DropdownMenuContent>
                    </DropdownMenu>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardContent>
      </Card>
    </div>
  )
}
