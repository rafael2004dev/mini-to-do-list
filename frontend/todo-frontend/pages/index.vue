<template>
  <div style="max-width: 500px; margin: auto; padding: 20px;">
    <h1>Mini To Do List</h1>

    <input v-model="newTask" placeholder="Digite uma tarefa" />
    <button @click="addTask">Adicionar</button>

    <ul>
      <li v-for="task in tasks" :key="task.id">
        <input type="checkbox" v-model="task.isCompleted" />
        {{ task.title }}
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const tasks = ref([])
const newTask = ref('')

const API_URL = 'https://localhost:3001//tarefas'

const fetchTasks = async () => {
  const response = await fetch(API_URL)
  tasks.value = await response.json()
}

const addTask = async () => {
  if (!newTask.value) return

  await fetch(API_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      title: newTask.value,
      isCompleted: false
    })
  })

  newTask.value = ''
  fetchTasks()
}

onMounted(fetchTasks)
</script>
