import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5000/api/studio'
})

export async function getStudioState() {
  const res = await api.get('/state')
  return res.data
}
