<template>
  <div style="max-width: 500px; margin: auto; padding: 20px;">
      <h1> <i> Mini To Do List - MoreResults </i></h1>
    
      <hr>

    <p> Bem-vindo(a),nesse website, você pode listar,criar e remover as tarefas a fazer!</p>

    <br>

    <input v-model="newTask" placeholder="Escreva uma Tarefa" />
    <button @click="addTask">Adicionar Tarefa</button>

    <ul>
      <li v-for="task in tasks" :key="task.id">
        <input type="checkbox" v-model="task.isCompleted" />
        {{ task.title }}
        <button @click="removeTask(task.id)">Remover Tarefa</button>
      </li>
    </ul>
  </div>

</template>

<script setup>
import { ref, onMounted } from 'vue'

const tasks = ref([])
const newTask = ref('')

const API_URL = 'https://localhost:7117/tasks'

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

const removeTask = async (id) => {
  await fetch(`${API_URL}/${id}`, {
    method: 'DELETE'
  })

  fetchTasks()
}

onMounted(fetchTasks)
</script>
